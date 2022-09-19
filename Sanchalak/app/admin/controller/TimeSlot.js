app.controller('TimeSlotCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage TimeSlot";
    $scope.UpdateBtnFlag = false;

    /*Reset TimeSlot*/
    $scope.resetTimeSlot = function () {
        $scope.TimeSlot = {};
        $scope.UpdateBtnFlag = false;
    };

     
    /*Get TimeSlot List*/
    $scope.TimeSlotGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/TimeSlot/TimeSlotGet',
            
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
                    $scope.TimeSlotTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.TimeSlotData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add TimeSlot Data*/
    $scope.TimeSlotAdd = function () {

        if ($scope.TimeSlot.DtStartTime == undefined || $scope.TimeSlot.DtStartTime == "" || $scope.TimeSlot.DtStartTime == null) {
            alert("Please select Start Time..!");
        }
        else if ($scope.TimeSlot.DtEndTime == undefined || $scope.TimeSlot.DtEndTime == "" || $scope.TimeSlot.DtEndTime == null) {
            alert("Please select End Time..!");
        }
        else if ($scope.TimeSlot.TimeSlotName == undefined || $scope.TimeSlot.TimeSlotName == "" || $scope.TimeSlot.TimeSlotName == null) {
            alert("Please Add Time Slot..!");
        }
        else if ($scope.TimeSlot.DtStartTime >= $scope.TimeSlot.DtEndTime) {
            alert("Start time is grater than End time..!");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/TimeSlot/TimeSlotAdd',
                data: $scope.TimeSlot,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.TimeSlotGet();
                        $scope.TimeSlot = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify TimeSlot Data*/
    $scope.modifyTimeSlotData = function (data) {
      
        $scope.showFormFlag = true;
        $scope.TimeSlot = data;
        $(window).scrollTop(0);
        $scope.UpdateBtnFlag = true;

        var splitDateStartTime = $scope.TimeSlot.StartTimeView.split(':');
        var splitDateEndTime = $scope.TimeSlot.EndTimeView.split(':');

        $scope.TimeSlot.DtStartTime = new Date(1970, 0, 1, splitDateStartTime[0], splitDateStartTime[1], splitDateStartTime[2]);
        $scope.TimeSlot.DtEndTime = new Date(1970, 0, 1, splitDateEndTime[0], splitDateEndTime[1], splitDateEndTime[2]);

      
    };

    /*Update TimeSlot Data*/
    $scope.TimeSlotEdit = function () {

        if ($scope.TimeSlot.DtStartTime == undefined || $scope.TimeSlot.DtStartTime == "" || $scope.TimeSlot.DtStartTime == null) {
            alert("Please select Start Time..!");
        }
        else if ($scope.TimeSlot.DtEndTime == undefined || $scope.TimeSlot.DtEndTime == "" || $scope.TimeSlot.DtEndTime == null) {
            alert("Please select End Time..!");
        }
        else if ($scope.TimeSlot.TimeSlotName == undefined || $scope.TimeSlot.TimeSlotName == "" || $scope.TimeSlot.TimeSlotName == null) {
            alert("Please Add Time Slot..!");
        }
        else if ($scope.TimeSlot.DtStartTime >= $scope.TimeSlot.DtEndTime) {
            alert("Start time is grater than End time..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/TimeSlot/TimeSlotEdit',
                data: $scope.TimeSlot,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.resetTimeSlot();
                        $scope.TimeSlotGet();
                        $scope.TimeSlot = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete TimeSlot Data*/
    $scope.TimeSlotDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.TimeSlot = data;

            $http({
                method: 'POST',
                url: 'api/TimeSlot/TimeSlotDelete',
                data: $scope.TimeSlot,
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
                        $scope.TimeSlotGet();
                        $scope.TimeSlot = {};
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

});