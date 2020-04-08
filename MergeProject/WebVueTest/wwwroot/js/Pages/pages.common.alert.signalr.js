const hubCommonConnection = new signalR.HubConnectionBuilder()
    .withUrl("/common")
    .build();
$(function () {
    try {
        hubCommonConnection.on("ChangeModel", function (data) {
            //alert(data);
            console.log(data);
            $("#alert-title").text(data);
        });


        try {
            console.log("start");
            hubCommonConnection.start();
        }
        catch (e) {
            console.log(e);
            $("#alert-title").text(e);
        }


        if (!(commonSignalGroup === null) && hubCommonConnection.state != "Connected") {
            setTimeout(function () {
                console.log("AddGroup", hubCommonConnection);
                hubCommonConnection.invoke("AddGroup", commonSignalGroup);
            }, 200);
        }
    }
    catch (e) {
        hubCommonConnection = null
        console.log(e);
    }
});