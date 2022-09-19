app.controller('ProgrammeModeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Programme Mode Details";   

    $scope.ProgrammeModeTableParams = new NgTableParams({
    }, {
            dataset: $scope.getProgrammeMode
    });

    /*Reset Programme Mode*/
    $scope.resetProgrammeMode = function () {
        $scope.ProgrammeMode = {};
    };

    /*Get Programme Mode List*/
    $scope.getProgrammeMode = function () {
        
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstProgrammeMode/MstProgrammeModeListGet',
            
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
                    
                    $scope.ProgrammeModeTableParams = new NgTableParams({
                    }, {
                            dataset: response.obj
                        
                    });                    
                }                
                /*for (i = 0; i < response.obj.Id; i++) { alert(response.obj[i].Id);}*/                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Programme Mode*/
    $scope.addProgrammeMode = function () {        

        if ($scope.ProgrammeMode.ProgrammeModeName === null || $scope.ProgrammeMode.ProgrammeModeName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeMode/MstProgrammeModeAdd',
                data: $scope.ProgrammeMode,
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
                        $scope.ProgrammeMode = {};
                        $scope.getProgrammeMode();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Update Programme Mode*/
    $scope.editProgrammeMode = function () {

        if ($scope.ProgrammeMode.ProgrammeModeName === null || $scope.ProgrammeMode.ProgrammeModeName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeMode/MstProgrammeModeEdit',
                data: $scope.ProgrammeMode,
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
                        $scope.getProgrammeMode();
                        $scope.ShowFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Programme Mode*/
    $scope.deleteProgrammeMode = function (ev,data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgrammeMode = data;
                
                $http({
                    method: 'POST',
                    url: 'api/MstProgrammeMode/MstProgrammeModeDelete',
                    data: $scope.ProgrammeMode,
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
                            $scope.getProgrammeMode();
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
           
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Modify Programme Mode Data*/
    $scope.modifyProgrammeModeData = function (data) {
        $scope.ShowFormFlag = true;
        $scope.ProgrammeMode = data;
        $(window).scrollTop(0);
    };

    /*Add New Programme Mode*/
    $scope.newProgrammeModeAdd = function (data) {
        $state.go('ProgrammeModeAdd');
    };

    /*Back to Edit Page of Programme Mode*/
    $scope.backToList = function () {
        $state.go('ProgrammeModeEdit');
    };     

    /*Active Enable Programme Mode*/
    $scope.ShowProgrammeMode = function (data) {

        $scope.newProgrammeMode = data;
       
        $http({
            method: 'POST',
            url: 'api/MstProgrammeMode/MstProgrammeModeIsActive',
            data: $scope.newProgrammeMode,
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
                    $scope.getProgrammeMode();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Programme Mode*/
    $scope.HideProgrammeMode = function (data) {

        $scope.newProgrammeMode = data;
             
        $http({
            method: 'POST',
            url: 'api/MstProgrammeMode/MstProgrammeModeIsSuspended',
            data: $scope.newProgrammeMode,
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
                    $scope.getProgrammeMode();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };   

});