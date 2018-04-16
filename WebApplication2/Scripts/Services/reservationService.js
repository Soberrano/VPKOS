webApp.factory("Reservation", [
    "$rootScope",
    "HttpRequest",
    "RequestPromise",
    "Tools",
function (
    $rootScope,
    httpRequest,
    requestPromise,
    tools
) {

    var service = {

        getOrder: function (orderId) {
            return requestPromise({
                method: "POST",
                url: "/api/reservation/getOrder",
                params: {
                    orderId: orderId
                }
            });
        }
    };

    return service;
}]);