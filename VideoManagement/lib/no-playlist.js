(function ($, undefined) {

    var createPlayer = function ($player, playlist) {
        flowplayer.conf.share = false;
        flowplayer.conf.hlsjs = {
            maxBufferSize: 1
        };

        var api = flowplayer($player, {
            clip: {
                ima: {
                    ads: [{
                        time: 0,
                        adTag: "https://pubads.g.doubleclick.net/gampad/ads?sz=640x480&iu=/124319096/external/single_ad_samples&ciu_szs=300x250&impl=s&gdfp_req=1&env=vp&output=vast&unviewed_position_start=1&cust_params=deployment%3Ddevsite%26sample_ct%3Dlinear&correlator="
                    }, {
                        time: 10,
                        adTag: "https://pubads.g.doubleclick.net/gampad/ads?sz=640x480&iu=/48717572/fp-overlay&ciu_szs&impl=s&gdfp_req=1&env=vp&output=xml_vast2&unviewed_position_start=1&url=[referrer_url]&description_url=https%3A%2F%2Fflowplayer.org&correlator=[timestamp]"
                    }],

                },
                sources: [
                    {
                        type: "application/x-mpegurl",
                        src: playlist
                    }
                ]
            }
        });
        api.on("pause", function (e) {
            $player.removeClass("player-content-back");
        });
    };

    var init = function () {
        var $player = $(".ninisite-player");
        var playlist = $player.data("playlist");
        var processStatus = $player.data('processstatus');

        switch (processStatus) {
            case 0:
                $("#video-status").find("div").html("در حال پردازش");
                break;
            case 1:

                createPlayer($player, playlist);
                break;
            case 2:
                $("#video-status").find("div").html("در حال پردازش");
                break;
            case 3:
                $("#video-status").find("div").html("در حال پردازش");
                break;
            default:
                $("#video-status").find("div").html("در حال پردازش");
                break;
        }
    };

    $(function () {

        init();

    });

})(jQuery);

