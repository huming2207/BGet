# B-Get

B-Get, yet another unofficial Bilibili video downloader library & app.

CI Test (AppVeyor, master): [![Build status](https://ci.appveyor.com/api/projects/status/qlsk5u27bsgdkalq/branch/master?svg=true)](https://ci.appveyor.com/project/huming2207/bget/branch/master)

### [For Chinese README, please refer to this link.](README.md) 

### [中文版说明见此](README.md)

## Intro 

[Bilibili](https://www.bilibili.com) is a famous Chinese video sharing website. This project is a tool to download their videos to users' local devices. Sometimes if you watching videos on Bilibili outside mainland China, the video loading/caching speed will be extremely slow. This tool can help you directly download the videos and then you can watch them locally.

This project will planning to provide proxying functions to optimize the download process. But it will not provide any hacking functions for those videos with limited copyrights.

**Never re-post the downloaded video unless you have granted by the authors, copyright holders and/or Bilibili official team.**

## To-do list

### Video

- [x] Content ID parsing
- [x] Single video URL parsing
- [ ] Multiple (batch) video URL parsing
- [x] Video search result parsing
- [ ] Flash video merging

### User

- [x] User information parsing (e.g. user name and non-pirvate details)
- [x] User search result parsing
- [x] User upload record parsing 

### Search

- [x] All search functions

### Miscellaneous stuff

- [x] Some unit tests
- [ ] Exception handling

### Downloading

- [ ] Basic download functions
- [ ] Multi-threaded downloading
- [ ] Download via proxy

### End-user apps

- [x] .Net Core Commandline app (demo)
- [ ] Windows 10 UWP app
- [ ] Windows classic desktop app (WPF)
- [ ] macOS app (Xamarin)


## Credit

- Bilibili
- You-Get, for some crawling logics
- Someone who mentioned this site to me lol...