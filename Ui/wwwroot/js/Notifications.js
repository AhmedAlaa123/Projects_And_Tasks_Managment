"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7087/notifyHub?clientId=1", {
    //accessTokenFactory: () => $('#token').val(),
    headers: {
        "ClientId":1
    }
}).withAutomaticReconnect(1000).build();
connection.serverTimeOutInMilliseconds=50000;
 connection.start().then(function () {
    // refreshNotifcation()
}).catch(function (err) {
  
});

connection.on("ReceiveNotification", function (notification) {
    console.log(notification)

    const li = $(`
                <li class="list-group-item d-flex justify-content-between align-items-center mb-1">
                    <span>${notification.messageContent}</span>
                    <div>
                        <button class="btn btn-success btn-sm accept-btn" data-id="${notification.id}">
                            Accept
                        </button>
                        <button class="btn btn-danger btn-sm reject-btn" data-id="${notification.id}">
                            Reject
                        </button>
                    </div>
                </li>
            `);

    $("#Notifications").append(li);
    $(".accept-btn").off("click").on("click", function () {
        const id = $(this).data("id");
        console.log("Accepted notification:", id);

        connection.invoke("UpdateNotificationStatus", { id: id, clientId: 1, status: 5 })
            .catch(err => console.error(err));
    });

    $(".reject-btn").off("click").on("click", function () {
        const id = $(this).data("id");


        connection.invoke("UpdateNotificationStatus", { id: id, clientId: 1, status: 4 })
            .catch(err => console.error(err));
    });
});
connection.on("ReceiveNotifications", function (notifications) {
    console.log(notifications);
    if (notifications) {
        $("#Notifications").empty();
        for (let i = 0; i < notifications.length; i++) {
            const notif = notifications[i];

            const li = $(`
                <li class="list-group-item d-flex justify-content-between align-items-center mb-1">
                    <span>${notif.messageContent}</span>
                    <div>
                        <button class="btn btn-success btn-sm accept-btn" data-id="${notif.id}">
                            Accept
                        </button>
                        <button class="btn btn-danger btn-sm reject-btn" data-id="${notif.id}">
                            Reject
                        </button>
                    </div>
                </li>
            `);

            $("#Notifications").append(li);
        }

        // Attach click handlers
        $(".accept-btn").off("click").on("click", function () {
            const id = $(this).data("id");
            console.log("Accepted notification:", id);

            connection.invoke("UpdateNotificationStatus", { id: id, clientId: 1, status: 5 })
                .catch(err => console.error(err));
        });

        $(".reject-btn").off("click").on("click", function () {
            const id = $(this).data("id");
             

            connection.invoke("UpdateNotificationStatus", { id: id, clientId: 1, status:4 })
                .catch(err => console.error(err));
        });
    }
});
connection.on("UpdateStatusResult", function (notification) {
    connection.invoke("GetClientNotifications", 1) // client id is 1 this will by daynamic from token
        .catch(err => console.error(err));
});