$(() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/bookingHub")
        .build();

    connection.start()
        .then(() => {
            console.log("SignalR connected.");
        })
        .catch(err => {
            console.error("SignalR connection error: ", err.toString());
        });

    connection.on("ReceiveMessageWithData", async function (data) {
        console.log("Message received with data: ", data);
        await LoadRooms(data);
    });
    connection.on("ReceiveMessage", async function (message) {
        console.log("Message received: ", message);
    });

    async function LoadRooms(data) {
        var tr = '';
        $.each(data.$values, (k, v) => {
            tr += `<tr>
                    <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">${v.roomId}</th>
                    <td class="px-6 py-4">
                        <a asp-page="./Details" asp-route-id="@item.RoomId">${v.roomNumber}</a>
                    </td>
                    <td class="px-6 py-4">${v.roomMaxCapacity}</td>
                    <td class="px-6 py-4">${v.roomStatus ? "<span class='text-green-500'>Active</span>" : "<span class='text-red-500'>Inactive</span>"}</td>
                    <td class="px-6 py-4">${v.roomPricePerDay}</td>
                    <td class="px-6 py-4">${v.roomType?.roomTypeName ? v.roomType?.roomTypeName : 'Deluxe Room'}</td>
                    <td class="px-6 py-4">
                        <a asp-page="./Edit" asp-route-id="@item.RoomId">
                            <button type="submit" class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                                Update
                            </button>
                        </a>
                        <a asp-page="./Delete" asp-route-id="@item.RoomId">
                            <button type="submit" class="text-white bg-red-500 hover:bg-red-600 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-blue-800 ml-2">
                                Delete
                            </button>
                        </a>
                    </td>
                </tr>`
        })
        $("#tableBody").html(tr);
    };
});
