# Nginx configuration dotnet parser

[![.NET](https://github.com/JustWei-OST/nginx-dotnet-parser/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JustWei-OST/nginx-dotnet-parser/actions/workflows/dotnet.yml)

这是一个基于`.net netstandard2.1` 的轻量级Nginx配置文件解析器，用于解析Nginx配置文件，提取\修改配置信息，生成配置文件等。

## 特征

- 使用 `ANTLR4` 将Nginx配置转换成 `AST` 树
- 支持合并 `include` 的配置文件
- 提供了一些便捷的查询方法,方便获取配置信息
- 您也可以对配置对象(参数、块、注释)进行修改,例如:添加、删除、修改.
- 支持 `#注释` 的解析
- 支持对配置项(块)调整顺序
- 可以将配置项,或者整个配置树,转换成Nginx配置文件,供Nginx使用

## 安装

```csharp
Install-Package NginxDotnetParser
```

## 示例

请先看看单元测试,里面有各场景的使用示例,详细的文档,后续会补充.

## 鸣谢

本项目受到了以下项目的启发,在此表示感谢:
- [nginx-java-parser](https://github.com/odiszapc/nginx-java-parser)

## -

欢迎大家提出宝贵的意见和建议,如果您觉得这个项目对您有帮助,请给我一个star,谢谢!
