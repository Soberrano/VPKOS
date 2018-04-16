webApp.controller("HallCtrl", ["$rootScope", "$scope", function ($rootScope, $scope) {
    $scope.Hall = {
        id: 1,
        Sectors: [
            {
                id: 1,
                Rows: [
                    {
                        id: 1,
                        label: 1,
                        Seats: [
                            { id: 1, label: 1, Price: 1000 },
                            { id: 2, label: 2, Price: 1000 },
                            { id: 3, label: 3, Price: 1000 }
                        ]
                    },
                    {
                        id: 2,
                        label: 2,
                        Seats: [
                            { id: 4, label: 1, Price: 500 },
                            { id: 5, label: 2, Price: 500 },
                            { id: 6, label: 3, Price: 500 }
                        ]
                    }
                ]
            },
            {
                id: 2,
                Rows: [
                    {
                        id: 3,
                        label: 1,
                        Seats: [
                            { id: 7, label: 1, Price: 1000 },
                            { id: 8, label: 2, Price: 1000 },
                            { id: 9, label: 3, Price: 1000 }
                        ]
                    },
                    {
                        id: 4,
                        label: 2,
                        Seats: [
                            { id: 10, label: 1, Price: 500 },
                            { id: 11, label: 2, Price: 500 },
                            { id: 12, label: 3, Price: 500 }
                        ]
                    }
                ]
            }
        ]
    }
}]);