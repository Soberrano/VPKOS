webApp.controller("DogCtrl", ["$rootScope", "$scope", "RequestPromise", function ($rootScope, $scope, requestPromise) {
    $scope.getDogs = function () {
        return requestPromise(
            {
                method: "POST",
                url: "/api/dog/getDogs"
            }
        );
    }
    $scope.Dogs;
    $scope.getDogs().then(function (data) {
        $scope.Dogs = data;
    })

    $scope.addDog = function () {
        return requestPromise(
            {
                method: "POST",
                url: "/api/dog/addDogs"
            }
        );

    }
}]);