# Bogger

Bogger, 一个基于.NET Core和Widnows 10 UWP的Bilibili视频下载器。

[For English README, please refer to this link](TODO)

## 简介

没错，这破玩意儿就是个B站视频下载器而已。叫他Bogger纯属娱乐，[请勿上纲上线。](https://www.zhihu.com/question/28644225/answer/41600773)

本项目是为了在恶劣网络环境下提升B站观影体验（是的我必须得黑一把土澳的[TPG](https://en.wikipedia.org/wiki/TPG_Telecom)），以及某些收集癖患者做离线备份而设计。

**本项目仅提供下载、缓冲功能，未经原作者、发行方或B站允许，请勿将下载过的视频转发至别处，否则……轻则被骂死全家，重则被告上法庭蹲监狱捡肥皂。所以，呵呵，有这种恶意转载想法的人，自己看着办吧。**

**本项目将会提供代理设置以便某些大神加速下载。但本项目不提供破解版权／收费限制的功能**

我既不粉二刺螈也不歧视二刺螈，所以App界面设计会尽量符合微软官方的设计语言，尽可能保持简洁不会加入任何二刺螈风格（好吧其实是我不会弄太复杂的UI开发啦）。

不过我也认为，作为男女比例5:1的工科院校学生，本院二刺螈死宅比例不小，因此有必要跟二刺螈死宅划清界限，总不能就这么轻易地“同流合污”、“泯然众人”了嘛。**而其坠吼的办法就是，当他们在B站看二刺螈妹子看得舔屏的时候，我在享受正常三次元的生活（搞不懂为什么看个画出来的人欲望还那么强烈），包括在写B站爬虫/下载器。单从这一点来说，我就比他们高的不知哪去了。😂**

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

异步方法`GetVideoInfo(avId)`会返回VideoInfo类，内含上述信息，直接用就行了。是不是很简单啊？

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
