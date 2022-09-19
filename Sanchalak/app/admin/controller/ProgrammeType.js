app.controller('ProgrammeTypeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Programme Type Details";

    $scope.ProgrammeTypeTableParams = new NgTableParams({
    }, {
        dataset: $scope.getProgrammeType
    });

    /*Reset Programme Type*/
    $scope.resetProgrammeType = function () {
        $scope.ProgrammeType = {};
    };

    /*Get Programme Type List*/
    $scope.getProgrammeType = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstProgrammeType/MstProgrammeTypeListGet',
            
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

                    $scope.ProgrammeTypeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj

                    });
                }              

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Programme Type*/
    $scope.addProgrammeType = function () {

        if ($scope.ProgrammeType.ProgrammeTypeName === null || $scope.ProgrammeType.ProgrammeTypeName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeType/MstProgrammeTypeAdd',
                data: $scope.ProgrammeType,
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
                        $scope.ProgrammeType = {};
                        $scope.getProgrammeType();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Update Programme Type*/
    $scope.editProgrammeType = function () {

        if ($scope.ProgrammeType.ProgrammeTypeName === null || $scope.ProgrammeType.ProgrammeTypeName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeType/MstProgrammeTypeEdit',
                data: $scope.ProgrammeType,
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
                        $scope.getProgrammeType();
                        $scope.ShowFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Programme Type*/
    $scope.deleteProgrammeType = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgrammeType = data;

            $http({
                method: 'POST',
                url: 'api/MstProgrammeType/MstProgrammeTypeDelete',
                data: $scope.ProgrammeType,
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
                        $scope.getProgrammeType();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Modify Programme Type Data*/
    $scope.modifyProgrammeTypeData = function (data) {
        $scope.ShowFormFlag = true;
        $scope.ProgrammeType = data;
        $(window).scrollTop(0);
    };

    /*Add New Programme Type*/
    $scope.newProgrammeTypeAdd = function (data) {
        $state.go('ProgrammeTypeAdd');
    };

    /*Back to Edit Page of Programme Type*/
    $scope.backToList = function () {
        $state.go('ProgrammeTypeEdit');
    };

    /*Active Enable Programme Type*/
    $scope.ShowProgrammeType = function (data) {

        $scope.newProgrammeType = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammeType/MstProgrammeTypeIsActive',
            data: $scope.newProgrammeType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getProgrammeType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Programme Type*/
    $scope.HideProgrammeType = function (data) {

        $scope.newProgrammeType = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammeType/MstProgrammeTypeIsSuspended',
            data: $scope.newProgrammeType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getProgrammeType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});