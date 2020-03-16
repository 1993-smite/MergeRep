const commentSelector = "#comments-container";

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
            var commentsArray = [{
                id: 1,
                created: '2015-10-01',
                content: 'Lorem ipsum dolort sit amet',
                fullname: 'Simon Powell',
                upvote_count: 2,
                user_has_upvoted: false
            }];
            let comm = comments.map(function (x) {
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
                };
            });
            success(comm);
        },
        postComment: function (data, success, error) {
            setTimeout(function () {
                success(saveComment(data));
            }, 500);
        },
        putComment: function (data, success, error) {
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
});

