# Console application to generate html files
.Net console application that can generate html files populated with data from yaml file.

## Usage
Place Your data in [templateData.yaml](https://github.com/FranekDev/html-template-generator/blob/main/src/HtmlTemplateGenerator/templateData.yaml) file in this format:
```yaml
﻿name: name

companyBanner: bannerUrl

descriptions:
  - title: title
    text: text
    imageUrl: imageUrl

specification:
  title: title
  text: |
    Key: Value
    Key: Value

videos:
  - title: title
    description: description
    url: videoUrl

arrangementPhoto: photoUrl
```

You can specify how output html file will look in [TemplateHtmlBuilder.cs](https://github.com/FranekDev/html-template-generator/blob/main/src/HtmlTemplateGenerator/Builder/TemplateBuilder.cs) file

After that You can run this app via these commands:
```bash
dotnet build
cd .\src\HtmlTemplateGenerator\
dotnet run
```

Generated files will be available in 'Templates' directory

### Tech stack
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
