﻿const commentSelector = "#comments-container";

let commentEl; 
let commentElsuccess; 

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

function convertToComment(x) {
    return {
        id: x.id,
        parent: x.parentId < 1 ? null : x.parentId,
        content: x.text,
        creator: x.createdUser.id,
        pings: [],
        fullname: x.createdUser.login,
        created: moment(x.createDt).toDate(),
        modified: moment(x.updateDt).toDate(),
        created_by_admin: true,
        created_by_current_user: false,
        upvote_count: 0,
        user_has_upvoted: false,
        is_new: false
    }
}

$(function () {
    /*var appComments = new Vue({
        el: commentSelector,
        data: comments
    });*/
    var saveComment = function (data) {

        // Convert pings to human readable format
        $(Object.keys(data.pings)).each(function (index, userId) {
            var fullname = data.pings[userId];
            var pingText = '@' + fullname;
            data.content = data.content.replace(new RegExp('@' + userId, 'g'), pingText);
        });

        return data;
    }
    $(commentSelector).comments({
        currentUserId: model.id,
        roundProfilePictures: true,
        textareaRows: 1,
        enableAttachments: true,
        enableHashtags: true,
        enablePinging: true,
        searchUsers: function (term, success, error) {
            setTimeout(function () {
                success(users.filter(function (user) {
                    var containsSearchTerm = user.fullname.toLowerCase().indexOf(term.toLowerCase()) != -1;
                    var isNotSelf = user.id != 1;
                    return containsSearchTerm && isNotSelf;
                }));
            }, 500);
        },
        getComments: function (success, error) {
            let comm = comments.map(x => convertToComment(x));
            commentEl = this;
            commentElsuccess = success;
            success(comm);
        },
        postComment: function (data, success, error) {
            var comment = {
                CardId: model.id,
                Id: 0,
                ParentId: data.parent || 0,
                Text: data.content,
                CreateDt: data.created
            };
            console.log("post", data, comment);
            hubConnection.invoke("Send", comment);
            //hubConnection.invoke("SendText", data.content);
            /*setTimeout(function () {
                success(saveComment(data));
            }, 500);*/
        },
        putComment: function (data, success, error) {
            console.log("hello from hell");
            setTimeout(function () {
                success(saveComment(data));
            }, 500);
        },
        deleteComment: function (data, success, error) {
            setTimeout(function () {
                success();
            }, 500);
        },
        upvoteComment: function (data, success, error) {
            setTimeout(function () {
                success(data);
            }, 500);
        },
        uploadAttachments: function (dataArray, success, error) {
            setTimeout(function () {
                success(dataArray);
            }, 500);
        },
    });

    hubConnection.on("Send", function (data) {
        //$(commentSelector).comments({ putComment});
        console.log(Date.now(), data);
        let comments = saveComment(convertToComment(data));
        commentEl.addComment.call(commentEl,comments);
        //let comments = saveComment(convertToComment(data));
        //success(comments);
        //commentElsuccess.call(commentEl, comments);
        //commentEl.putComment(comments);
    });

    hubConnection.start();

    setTimeout(function () {
        hubConnection.invoke("AddGroup", model.id);
    }, 200)
    //
});