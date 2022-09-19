app.controller('ProgrammeLevelCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Programme Level Details";   

    $scope.ProgrammeLevelTableParams = new NgTableParams({
    }, {
            dataset: $scope.getProgrammeLevel
    });

    /*Reset Programme Level*/
    $scope.resetProgrammeLevel = function () {
        $scope.ProgrammeLevel = {};
    };

    /*Get Programme Level List*/
    $scope.getProgrammeLevel = function () {
        
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstProgrammeLevel/MstProgrammeLevelListGet',
            
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
                    
                    $scope.ProgrammeLevelTableParams = new NgTableParams({
                    }, {
                            dataset: response.obj
                        
                    });                    
                }
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };      

    /*Add Programme Level*/
    $scope.addProgrammeLevel = function () {        

        if ($scope.ProgrammeLevel.ProgrammeLevelName === null || $scope.ProgrammeLevel.ProgrammeLevelName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeLevel/MstProgrammeLevelAdd',
                data: $scope.ProgrammeLevel,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                        
                    }
                    else {
                        alert(response.obj);
                        $scope.ProgrammeLevel = {};
                        $scope.getProgrammeLevel();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Update Programme Level*/
    $scope.editProgrammeLevel = function () {

        if ($scope.ProgrammeLevel.ProgrammeLevelName === null || $scope.ProgrammeLevel.ProgrammeLevelName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeLevel/MstProgrammeLevelEdit',
                data: $scope.ProgrammeLevel,
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
                        $scope.getProgrammeLevel();
                        $scope.ShowFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Programme Level*/
    $scope.deleteProgrammeLevel = function (ev,data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgrammeLevel = data;
                
                $http({
                    method: 'POST',
                    url: 'api/MstProgrammeLevel/MstProgrammeLevelDelete',
                    data: $scope.ProgrammeLevel,
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
                            $scope.getProgrammeLevel();
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
           
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Modify Programme Level Data*/
    $scope.modifyProgrammeLevelData = function (data) {
        $scope.ShowFormFlag = true;
        $scope.ProgrammeLevel = data;
        $(window).scrollTop(0);
    };

    /*Add New Programme Level*/
    $scope.newProgrammeLevelAdd = function (data) {
        $state.go('ProgrammeLevelAdd');
    };

    /*Back to Edit Page of Programme Level*/
    $scope.backToList = function () {
        $state.go('ProgrammeLevelEdit');
    };     

    /*Active Enable Programme Level*/
    $scope.ShowProgrammeLevel = function (data) {

        $scope.newProgrammeLevel = data;
       
        $http({
            method: 'POST',
            url: 'api/MstProgrammeLevel/MstProgrammeLevelIsActive',
            data: $scope.newProgrammeLevel,
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
                    $scope.getProgrammeLevel();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Programme Level*/
    $scope.HideProgrammeLevel = function (data) {

        $scope.newProgrammeLevel = data;
             
        $http({
            method: 'POST',
            url: 'api/MstProgrammeLevel/MstProgrammeLevelIsSuspended',
            data: $scope.newProgrammeLevel,
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
                    $scope.getProgrammeLevel();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };   

});