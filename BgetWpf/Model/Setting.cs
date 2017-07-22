using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BgetWpf.Model
{
    public class Setting
    {
        [JsonProperty("video")]
        public VideoSetting VideoSetting { get; set; }

        [JsonProperty("general")]
        public GeneralSetting GeneralSetting { get; set; }

        [JsonProperty("torrent")]
        public TorrentSetting TorrentSetting { get; set; }

        [JsonProperty("aria")]
        public AriaSetting AriaSetting { get; set; }
    }

    public class VideoSetting
    {
        [JsonProperty("prefer_flv")]
        public bool PreferFlv { get; set; }

        [JsonProperty("prefer_mp4")]
        public bool PreferMp4 { get; set; }

        [JsonProperty("prefer_hq")]
        public bool PreferHighQuality { get; set; }

        [JsonProperty("prefer_mq")]
        public bool PreferMediumQuality { get; set; }

        [JsonProperty("prefer_lq")]
        public bool PreferLowQuality { get; set; }
    }

    public class GeneralSetting
    {
        [JsonProperty("auto_concurrent_task")]
        public bool AutoConcurrentTask { get; set; }

        [JsonProperty("max_conn_task")]
        public string MaxConcurrentTask { get; set; }

        [JsonProperty("max_conn_per_server")]
        public string MaxConnectionPerServer { get; set; }

        [JsonProperty("split_amount")]
        public string SplitAmount { get; set; }

        [JsonProperty("disk_cache")]
        public string DiskCache { get; set; }

        [JsonProperty("up_limit")]
        public string UploadLimit { get; set; }

        [JsonProperty("down_limit")]
        public string DownloadLimit { get; set; }

        [JsonProperty("down_path")]
        public string DownloadPath { get; set; }
    }

    public class TorrentSetting
    {
        [JsonProperty("dht")]
        public bool EnableDht { get; set; }

        [JsonProperty("pex")]
        public bool EnablePex { get; set; }

        [JsonProperty("lpd")]
        public bool EnableLpd { get; set; }

        [JsonProperty("allow_encrypt")]
        public bool EnableEncrypt { get; set; }

        [JsonProperty("force_encrypt")]
        public bool ForceEncrypt { get; set; }

        [JsonProperty("check_before_seed")]
        public bool CheckBeforeSeeding { get; set; }

        [JsonProperty("seed_broken")]
        public bool SeedWhenBroken { get; set; }

        [JsonProperty("peer_id_perfix")]
        public string PeerIdPerfix { get; set; }

        [JsonProperty("bt_ua")]
        public string TorrentUserAgent { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }

        [JsonProperty("max_peers")]
        public string MaxPeers { get; set; }

        [JsonProperty("seed_ratio")]
        public string SeedRatio { get; set; }

    }

    public class AriaSetting
    {
        [JsonProperty("external_aria")]
        public bool EnableExternalAria { get; set; }

        [JsonProperty("aria_arch")]
        public string AriaArch { get; set; }

        [JsonProperty("aria_rpc_url")]
        public string AriaRpcUrl { get; set; }

        [JsonProperty("extra_args")]
        public string ExtraArgs { get; set; }
    }
}
