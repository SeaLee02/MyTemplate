var sea = {
    ajax: function (userOptions) {
        //var $ = layui.$;
        userOptions = userOptions || {};
        if (!userOptions.type) {
            userOptions.type = "POST";
        }
        return $.Deferred(function ($dfd) {
            $.ajax(userOptions)
                .done(function (data, textStatus, jqXHR) {
                    //if (userOptions.loadingIndex) {
                    //    layer.close(userOptions.loadingIndex);
                    //}
                    if (data.errcode != 0) {
                        if (userOptions.error) {
                            userOptions.error();
                        }
                        //layer.msg(data.errmsg);
                    } else {
                        $dfd.resolve(data);
                        userOptions.success && userOptions.success(data);
                    }
                }).fail(function (jqXHR) {

                    //if (userOptions.loadingIndex) {
                    //    layer.close(userOptions.loadingIndex);
                    //}
                });
        });
    },
    getNowDate: function getNowDate() {
        var myDate = new Date();
        var year = myDate.getFullYear();
        var month = myDate.getMonth() + 1;
        var date = myDate.getDate();
        var h = myDate.getHours();
        var m = myDate.getMinutes();
        var s = myDate.getSeconds();

        var nowDate = year + '-' + this.p(month) + "-" + this.p(date) + " " + this.p(h) + ':' + this.p(m) + ":" + this.p(s);
        return nowDate;
    },
    getQueryString: function getQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURI(r[2]); return null;
    },
    getCookie: function (cookieName) {
        return $.cookie(cookieName);
    },
    getCookieObj: function (cookieName) {
        var cookie = this.getCookie(cookieName);
        if (cookie) {
            return JSON.parse(cookie);
        } else {
            return null;
        }
    },
    setCookie: function (cookieName, cookieValue, expiresDay) {
        if (!expiresDay) {
            expiresDay = 1;
        }
        $.cookie(cookieName, cookieValue, { expires: expiresDay, path: '/' });
    },
    deleteCookie: function myfunction(cookieName) {
        $.cookie(cookieName, '', { expires: -1, path: '/' });
    },
    setUserInfo: function (data) {
        data = JSON.stringify(data);
        this.setCookie("UserName", data, 7);
    },
    getUserInfo: function () {
        return this.getCookieObj("UserName");
    },
    deleteUserInfo: function () {
        this.deleteCookie("UserName");
    },
    parseQueryString: function (paramData) {
        if (!(typeof paramData == "string")) {
            return paramData;
        }
        var obj = {};
        var keyvalue = [];
        var key = "",
            value = "";
        var paraString = paramData.split("&");
        for (var i = 0; i < paraString.length; i++) {
            keyvalue = paraString[i].split("=");
            key = keyvalue[0];
            value = keyvalue[1];
            if (/^\d+-\d+-\d+$/.test(value)) {
                value = value + " 00:00:00";
            }
            if (value != "") {
                obj[key] = decodeURIComponent(value, true);
            }

        }
        return obj;
    },
    getAuthorizationCookie: function () {
        var cookie = $.cookie('Abp.AuthToken');
        if (!cookie) {
            cookie = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMDAxIiwianRpIjoiN2NkNjE2MWQtY2Q2Zi00MmM0LTk0NTgtNTg3Mzc1NjZkMzc4IiwiaWF0IjoxNTQyNjM1NTM4LCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsIm5iZiI6MTU0MjYzNTUzOCwiZXhwIjoxNTQ4NjM1NDc4LCJpc3MiOiJNeUFCUFZ1ZUNvcmUiLCJhdWQiOiJNeUFCUFZ1ZUNvcmUifQ.cUAoeEiF1OLC8TZ14Cxfk1ZJc15omxmJxTRCEa3tD4c";
        } else {
            cookie = "Bearer " + cookie;
        }

        if (!cookie) {
            return null;
        } else {
            return cookie
        }
    }
};