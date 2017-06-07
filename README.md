# B-Get

B-Get, 一个基于.NET Core的Bilibili视频下载器。

[For English README, please refer to this link](TODO)

## 简介

没错，这破玩意儿就是个B站视频下载器而已。

本项目是为了在恶劣网络环境下提升B站观影体验（是的我必须得黑一把土澳的[TPG](https://en.wikipedia.org/wiki/TPG_Telecom)），以及某些收集癖患者做离线备份而设计。

**本项目仅提供下载、缓冲功能，未经原作者、发行方或B站允许，请勿将下载过的视频转发至别处，否则……轻则被骂死全家，重则被告上法庭蹲监狱捡肥皂。所以，呵呵，有这种恶意转载想法的人，自己看着办吧。**

**本项目将会提供代理设置以便某些大神加速下载。但本项目不提供破解版权／收费限制的功能。**

**本人不擅长UI设计，所以UI比较简陋，希望各位大神能来几个Pull Request哟。**

## 还差啥玩意儿没整完啊？

- [x] CID解析
- [x] 单个视频地址解析
- [ ] 多个视频地址解析
- [x] 单元测试
- [ ] 报错优化
- [ ] 多线程视频解析
- [x] 跨平台命令行app **（目前半完工，仅解析，未提供下载功能）**
- [ ] Windows 10 UWP App
- [ ] Windows 传统桌面app（WPF）
- [ ] macOS app (Xamarin)

## 所以你这破代码得咋用啊？

关于视频信息获取，目前提供如下信息：

- 视频CID （这里叫它ContentID）
- 视频标题（Title）
- 视频简介（Description）
- 视频作者（Author）
- 视频关键词（Tags）

举个栗子 🌰，若要获取这个视频的视频信息：[http://www.bilibili.com/video/av8403837](http://www.bilibili.com/video/av8403837) 就应该这么写：

```csharp
// Get video information for the one with AV ID av8403837
string avId = "av8403837";
var videoInfoCrawler = new VideoInfoCrawler();
var videoInfo = await vidInfoCrawler.GetVideoInfo(avId);
```

异步方法`GetVideoInfo(avId)`会返回VideoInfo类，内含上述信息，直接用就行了。是不是很简单啊？

若想获取下载地址，则需要继续这样操作（是的这是最新操作😂）：

```csharp
// Get video URLs
var videoUrlGrabber = new VideoUrlGrabber();
var videoUrl = await videoUrlGrabber.GetUrlBySingleContentId(videoInfo.ContentID, avId);
```

其中返回值`videoUrl`包括了两层List。详情请参见命令行demo。

## 致谢

- Bilibili
- [You-Get](https://github.com/soimort/you-get)项目的部分爬虫逻辑
- 某人给的灵感 😂 
