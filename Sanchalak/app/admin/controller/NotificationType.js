app.controller('NotificationTypeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage NotificationType";

    /*Reset Evaluation*/
    $scope.resetNotificationType = function () {
        $scope.NotificationType = {};
    };

    /*Get Evaluation List*/
    $scope.getNotificationType = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/NotificationType/NotificationTypeGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.NotificationTypeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Evaluation*/
    $scope.addNotificationType = function () {

        if ($scope.NotificationType.NotificationTypeName === null || $scope.NotificationType.NotificationTypeName === undefined) {
            alert("Enter Notification Type Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/NotificationType/NotificationTypeAdd',
                data: $scope.NotificationType,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.NotificationType = {};
                        $scope.getNotificationType();
                        $state.go('NotificationTypeEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify Evaluation Data*/
    $scope.modifyNotificationTypeData = function (data) {
        $scope.NotificationType = data;
        $scope.showFormFlag = true;

    };

    /*Update Evaluation*/
    $scope.editNotificationType = function () {
        if ($scope.NotificationType.NotificationTypeName === null || $scope.NotificationType.NotificationTypeName === undefined) {
            alert("Enter NotificationType Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/NotificationType/NotificationTypeUpdate',
                data: $scope.NotificationType,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        // $scope.newUser = {};
                        $scope.getNotificationType();
                        $scope.modifyUserFlag = false;
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Evaluation*/
    $scope.deleteNotificationType = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.NotificationType = data;

            $http({
                method: 'POST',
                url: 'api/NotificationType/NotificationTypeDelete',
                data: $scope.NotificationType,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert("your data deleted successfully");
                        $scope.getNotificationType();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };    

    /*Add New Evaluation*/
    $scope.newNotificationTypeAdd = function () {
        $state.go('NotificationTypeAdd');
    };

    /*Back to Edit Page of Evaluation*/
    $scope.backToList = function () {
        $state.go('NotificationTypeEdit');
    };

    /*Display Evaluation Data*/
    $scope.displayNotificationType = function (data) {
        $scope.NotificationType = data;
    };

    /*Active Enable Evaluation*/
    $scope.ShowNotificationType = function (data) {

        $scope.NotificationType = data;

        $http({
            method: 'POST',
            url: 'api/NotificationType/NotificationTypeIsActive',
            data: $scope.NotificationType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getNotificationType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Evaluation*/
    $scope.HideNotificationType = function (data) {

        $scope.NotificationType = data;

        $http({
            method: 'POST',
            url: 'api/NotificationType/NotificationTypeIsSuspended',
            data: $scope.NotificationType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getNotificationType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

