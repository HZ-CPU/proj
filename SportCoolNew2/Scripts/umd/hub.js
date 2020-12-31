
var clients = [];
//alert('test');
var chat;
$(function () {
    chat = $.connection.chat;
    console.info(chat);

    //顯示提示方法
    chat.client.showMessage = function (message) {
        alert(message);
    }

    //註冊顯示信息的方法
    chat.client.addMessage = function (message, connectionId) {
        //debugger
        if ($.inArray(connectionId, clients) == -1) {
            showWin(connectionId);
        }

        $("#messages" + connectionId).each(function () {
            $(this).append('<li>' + message + '</li>');
        })
    }

    //註冊顯示所有用户的方法
    chat.client.getUsers = function (data) {
        if (data) {
            var json = $.parseJSON(data);
            console.info(json);
            $("#users").html(" ");
            for (var i = 0; i < json.length; i++) {
                var html = '<li>用户名:' + json[i].Name + '<input type="button" connectionId="' + json[i].ConnectionID + '" id="' + json[i].ConnectionID + '" value="聊天" onclick="userChat(this)" />';
                $("#users").append(html);
            }
        }
    }

    //註冊顯示推出聊天提示的方法
    chat.client.exitUser = function (data) {
        alert(data);
    }

    //註冊顯示個人信息的方法
    chat.client.showId = function (data) {
        $("#conId").html(data);
        clients.push(data);
    }

    //獲取用户名稱
    // $('#userName').html(prompt('請輸入您的名稱', ''));56556

    //連接成功後獲取自己的信息
    $.connection.hub.start().done(function () {
        chat.server.getName($('#userName').val());
        //alert('test');
    });
});

//開始聊天
function userChat(obj) {
    var connectionId = $(obj).attr('connectionId');
    showWin(connectionId);
}

function showWin(connectionId) {
    clients.push(connectionId);
    var html = '<div style="float:left;margin-top:5px;margin-right: 5px;margin-bottom: 5px;border:1px solid #ff0000" id="' + connectionId + '" connectionId="' + connectionId + '">'+ '"聊天記錄如下:<button onclick="exitChat(this)">退出</button><ul id="messages' + connectionId + '"></ul><input type="text" /> <button onclick="sendMessage(this)">發送</button></div>';
    $("#userBox").append(html);
}
////開始聊天
//        function userChat(obj) {
//            var connectionId = $(obj).attr('connectionId');
//            showWin(connectionId);
//        }

//        function showWin(connectionId) {
//            clients.push(connectionId);
//            var html = '<div style="float:left;margin-top:5px;margin-right: 5px;margin-bottom: 5px;border:1px solid #ff0000" id="' + connectionId + '" connectionId="' + connectionId + '">' + connectionId + '"的房間聊天記錄如下:<button onclick="exitChat(this)">退出</button><ul id="messages' + connectionId + '"></ul><input type="text" /> <button onclick="sendMessage(this)">發送</button></div>';
//            $("#userBox").append(html);
//        }

//        function exitChat(btnObj) {
//            $(btnObj).parent().remove();
//            chat.server.exitChat(connectionId);
//}

//開始聊天
function userChat(obj) {
    var connectionId = $(obj).attr('connectionId');
    showWin(connectionId);
}

function showWin(connectionId) {
    clients.push(connectionId);
    var html = '<div style="float:left;margin-top:5px;margin-right: 5px;margin-bottom: 5px;border:1px solid #ff0000" id="' + connectionId + '" connectionId="' + connectionId + '">' + connectionId + '"的房間聊天記錄如下:<button onclick="exitChat(this)">退出</button><ul id="messages' + connectionId + '"></ul><input type="text" /> <button onclick="sendMessage(this)">發送</button></div>';
    $("#userBox").append(html);
}

function exitChat(btnObj) {
    $(btnObj).parent().remove();
    chat.server.exitChat(connectionId);
}


function exitChat(btnObj) {
    $(btnObj).parent().remove();
    chat.server.exitChat(connectionId);
}

//發送消息
function sendMessage(data) {
    var message = $(data).prev().val();
    var userObj = $(data).parent();
    var username = $("#userName").html();
    message = username + ":" + message;
    console.info($(userObj).attr("connectionId"));
    var targetConnectionId = $(userObj).attr("connectionId");
    chat.server.sendMessage(targetConnectionId, message);
    $(data).prev().val("");
}
