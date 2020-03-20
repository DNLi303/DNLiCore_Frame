

//function axioshelp(url, data) {
//    return new Promise((resolve, reject) => {
//        axios({
//            url: url,
//            method: 'post',
//            headers: { 'Authorization': localStorage.getItem("Authorization") },
//            data: data
//        }).then((res) => {
//            if (res.data.code == -999) {
//                window.location.href = '/login.html'
//            }
//            else {
//                resolve(res.data);
//            }
//        }).catch(function (error) {
//            reject(error);
//        });
//    });
//}

//function axiosadhelp(url, data) {
//    return new Promise((resolve, reject) => {
//        axios({
//            url: url,
//            method: 'post',
//            headers: { 'Authorization': localStorage.getItem("Authorization") },
//            data: data
//        }).then((res) => {
//            if (res.data.code == -999) {
//                window.location.href = '/adlogin.html'
//            }
//            else {
//                resolve(res.data);
//            }
//        }).catch(function (error) {
//            reject(error);
//        });
//    });
//}

function axioshelp(url, data) {
   
    return new Promise((resolve, reject) => {
        $.ajax({
            "type": "post",
            "url": url,
            "contentType": 'application/json; charset=utf-8',
            "headers": { 'Authorization': localStorage.getItem("Authorization") },
            "async": true,
            "data": JSON.stringify(data),
            "success": function (res) {
                //返回状态处理
                if (res.code == -999) {
                    window.location.href = '/login.html'
                }
                else {
                    resolve(res);
                }
            }
        });
    });
}



function axiosadhelp(url, data) {
    return new Promise((resolve, reject) => {
        $.ajax({
            "type": "post",
            "url": url,
            "contentType": 'application/json; charset=utf-8',
            "headers": { 'Authorization': localStorage.getItem("Authorization") },
            "async": true,
            "data": JSON.stringify(data),
            "success": function (res) {                
                //返回状态处理
                if (res.code == -999) {
                    window.location.href = '/login.html'
                }
                else {
                    resolve(res);
                }               
            }
        });
    });
}


//function axiosadhelp(url, data) {
//    $.ajax({
//        "type": "post",
//        "url": url,
//        "contentType": 'application/json; charset=utf-8',
//        "async": true,
//        "data": JSON.stringify(data),
//        "success": function (res) {
//            //返回状态处理
//            if (res.code == 200) {
//                if (successCallback) {
//                    successCallback(res);
//                }
//            } else if (res.code == 406) {
//                //跳转至登录
//                methods.toLogin();
//            } else {
//                if (errorCallback) {
//                    errorCallback(res)
//                } else {
//                    methods.prompt(res.msg);
//                }
//            }
//        }
//    });
//}

//var methods = {
//    //全站ajax请求状态处理
//    ajax: function (url, data, successCallback, errorCallback) {
//        $.ajax({
//            "type": "post",
//            "url": url,
//            "contentType": 'application/json; charset=utf-8',
//            "async": true,
//            "data": JSON.stringify(data),
//            "success": function (res) {
//                //返回状态处理
//                if (res.code == 200) {
//                    if (successCallback) {
//                        successCallback(res);
//                    }
//                } else if (res.code == 406) {
//                    //跳转至登录
//                    methods.toLogin();
//                } else {
//                    if (errorCallback) {
//                        errorCallback(res)
//                    } else {
//                        methods.prompt(res.msg);
//                    }
//                }
//            }
//        });
//    }
//}

