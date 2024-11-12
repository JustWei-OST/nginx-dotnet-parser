grammar Nginx;

@parser::header {}

@lexer::header {}

config returns [NgxConfig ret]
@init { $ret = new NgxConfig(); }
  :
  (
    statement { $ret.AddEntry($statement.ret); }
    | block   { $ret.AddEntry($block.ret); }
    | Comment { $ret.AddEntry(new NgxComment($Comment.text)); }
  )+
  ;

statement returns [NgxParam ret]
:
(
  rewriteStatement { $ret = $rewriteStatement.ret; }
  |
  genericStatement { $ret = $genericStatement.ret; }
  |
  regexHeaderStatement { $ret = $regexHeaderStatement.ret; }
)
';';

genericStatement returns [NgxParam ret]
@init { $ret = new NgxParam(); }
  :
  Value  { $ret.AddValue($Value.text); }
  (
    Value { $ret.AddValue($Value.text); }
    |
    r=regexp { $ret.AddValue($r.ret); }
  )*
  ;

regexHeaderStatement returns [NgxParam ret]
@init { $ret = new NgxParam(); }
  :
  // Use token definition for regexp-driven parameter name in Nginx config
  // See: http://nginx.org/en/docs/http/ngx_http_map_module.html
  REGEXP_PREFIXED { $ret.AddValue($REGEXP_PREFIXED.text); }
  Value  { $ret.AddValue($Value.text); }
  ;

block returns [NgxBlock ret]
@init { $ret = new NgxBlock(); }
  :
  (
    locationBlockHeader { $ret.GetTokens().AddRange($locationBlockHeader.ret); }
    |
    genericBlockHeader  { $ret.GetTokens().AddRange($genericBlockHeader.ret); }
  )
  Comment?
  '{'
  (
    statement { $ret.AddEntry($statement.ret); }
    |
    b=block { $ret.AddEntry($b.ret); }
    |
    if_statement { $ret.AddEntry($if_statement.ret); }
    |
    Comment { $ret.AddEntry(new NgxComment($Comment.text)); }
  )*
  '}'
  ;

genericBlockHeader returns [List<NgxToken> ret]
@init { $ret = new ArrayList<NgxToken>(); }
  :
  Value { $ret.Add(new NgxToken($Value.text)); }
  (
    Value { $ret.Add(new NgxToken($Value.text)); }
    |
    regexp { $ret.Add(new NgxToken($regexp.ret)); }
  )*;

if_statement returns [NgxIfBlock ret]
@init { $ret = new NgxIfBlock(); }
  :
  id='if' { $ret.AddValue(new NgxToken($id.text)); }
  if_body { $ret.GetTokens().AddRange($if_body.ret); }
  Comment?
  '{'
    (statement { $ret.AddEntry($statement.ret); } )*
  '}'
  ;

if_body  returns [List<NgxToken> ret]
@init { $ret = new ArrayList<NgxToken>(); }
  :
  '('
  Value  { $ret.Add(new NgxToken($Value.text)); }
  (Value { $ret.Add(new NgxToken($Value.text)); })?
  (
    Value { $ret.Add(new NgxToken($Value.text)); }
    |
    regexp { $ret.Add(new NgxToken($regexp.ret)); }
  )?
  ')'
  ;

regexp returns [String ret]
@init { $ret = ""; }
:
(
  id='\\.' { $ret += $id.text; }
  | id='^' { $ret += $id.text; }
  | Value { $ret += $Value.text; }
  | '(' r=regexp { $ret += "(" + $r.ret +  ")" ; } ')'
)+;

locationBlockHeader returns [List<NgxToken> ret]
@init { $ret = new ArrayList<NgxToken>(); }
  :
  id='location' { $ret.Add(new NgxToken($id.text)); }
  (Value { $ret.Add(new NgxToken($Value.text)); })?
  (
    Value { $ret.Add(new NgxToken($Value.text)); }
    |
    regexp { $ret.Add(new NgxToken($regexp.ret)); }
  )
;

rewriteStatement returns [NgxParam ret]
@init { $ret = new NgxParam(); }
  :
  id='rewrite' { $ret.AddValue($id.text); }
  (Value { $ret.AddValue($Value.text); } | regexp { $ret.AddValue($regexp.ret); }) Value { $ret.AddValue($Value.text); }
  (opt=('last' | 'break' | 'redirect' | 'permanent') { $ret.AddValue($opt.text); })?
  ;

//QUOTED_STRING
//: '"' (~('"' | '\\' | '\r' | '\n') | '\\' ('"' | '\\'))* '"';


Value: STR_EXT | QUOTED_STRING | SINGLE_QUOTED
;

STR_EXT
  :
  ([a-zA-Z0-9_/.,\-:=~+!?$&^*[\]@|#] | NON_ASCII)+;

Comment
    :
    '#' ~[\r\n]*;

REGEXP_PREFIXED
  : (RegexpPrefix)[a-zA-Z0-9_/.,\-:=~+!?$&^*[\]@|#)(]+
  ;

QUOTED_STRING
  :
  '"' StringCharacters? '"'
  ;

fragment RegexpPrefix : [~][*]?;

fragment StringCharacters : (~["\\] | EscapeSequence)+;

fragment NON_ASCII :  '\u0080'..'\uFFFF';

fragment
EscapeSequence
    :   '\\' [btnfr"'\\]?
    ;

SINGLE_QUOTED
:
'\'' ~['\\]* '\'';

WS
:
[ \t\n\r]+ -> skip ;