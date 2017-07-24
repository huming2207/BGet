# BrinkGet

### **[This is a project mainly served for Chinese guys. For English README, please refer to this link](README-en.md)**

**BrinkGet，一个闷声发大财的下载器。**

CI测试（AppVeyor, master）：[![Build status](https://ci.appveyor.com/api/projects/status/qlsk5u27bsgdkalq/branch/master?svg=true)](https://ci.appveyor.com/project/huming2207/bget/branch/master)

## 简介

没错，这破玩意儿就是个下载器。

本项目是为了在恶劣网络环境下提升某视频网站观影体验（是的我必须得黑一把土澳的[TPG](https://en.wikipedia.org/wiki/TPG_Telecom)），以及某些收集癖患者做离线备份而设计。

**本项目仅提供下载、缓冲功能，未经原作者、发行方允许，请勿将下载过的视频转发至别处，否则……轻则被骂死全家，重则被告上法庭蹲监狱捡肥皂。所以，呵呵，有这种恶意转载想法的人，自己看着办吧。**

**本项目将会提供代理设置以便某些大神加速下载。但本项目不提供破解版权／收费限制的功能。同时为避免有人恶意转载付费视频，未来开发计划不包含付费视频解析下载。**

**本人不擅长UI设计，所以UI比较简陋，希望各位大神能来几个Pull Request哟。**

## 还差啥玩意儿没整完啊？

### 视频

- [x] CID解析
- [x] 单个视频地址解析
- [ ] 多个视频地址解析
- [x] 视频搜索结果解析
- [ ] FLV视频合并

### 用户

- [x] 用户解析
- [x] 用户搜索结果解析
- [x] 用户视频解析

### 搜索

- [x] 全部搜索功能

### 杂项

- [x] 单元测试
- [ ] 异常（报错）处理优化
- [ ] 多线程视频解析

### 下载

- [ ] 基本下载功能
- [ ] 多线程下载
- [ ] 走代理

### 成品app

- [x] 跨平台命令行app **（目前半完工，仅解析，未提供下载功能）**
- [ ] Windows 10 UWP App
- [x] Windows 传统桌面app（WPF）**<-- 正在折腾**
- [ ] macOS app (Xamarin)

## 所以你这破代码得咋用啊？

### 视频获取

关于视频信息获取，目前提供如下信息：

- 视频CID （这里叫它ContentID）
- 视频标题（Title）
- 视频简介（Description）
- 视频作者（Author）
- 视频关键词（Tags）

举个栗子 🌰：

```csharp
// Get video information for the one with AV ID av8403837
string avId = "av8403837";
var videoInfoCrawler = new VideoInfoCrawler();
var videoInfo = await vidInfoCrawler.GetVideoInfo(avId);
```

异步方法`GetVideoInfo(avId)`会返回VideoInfo类，内含上述信息，直接用就行了。是不是很简单啊？

若想获取下载地址，则需要继续这样操作（是的这是最新操作）：

```csharp
// Get video URLs
var videoUrlGrabber = new VideoUrlGrabber();
var videoUrl = await videoUrlGrabber.GetUrlBySingleContentId(videoInfo.ContentID, avId);
```

其中返回值`videoUrl`包括了两层List。详情请参见命令行demo。

## 致谢

- Bilibili
- [You-Get](https://github.com/soimort/you-get)项目的部分爬虫逻辑
- 某人给的灵感 （至少让我练手了嘛 对不～？😂） 

## Licence
CC-BY-NC-SA 3.0 Australian Licence
