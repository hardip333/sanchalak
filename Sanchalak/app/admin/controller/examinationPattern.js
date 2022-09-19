app.controller('examinationPatternCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Examination Pattern Details";   

    /*Reset ExaminationPattern*/
    $scope.resetExaminationPattern = function () {
        $scope.examinationPattern = {};
    };

    /*Get ExaminationPattern Data*/
    $scope.getExaminationPattern = function () {
        
        var data = new Object();

        $http({
            method: 'GET',
            url: 'api/MstExaminationPattern/MstExaminationPatternGet',
            
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
                    
                    $scope.examinationPatternTableParams = new NgTableParams({
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

    /*Add ExaminationPattern*/
    $scope.addExaminationPattern = function () {        

        if ($scope.examinationPattern.ExaminationPatternName === null || $scope.examinationPattern.ExaminationPatternName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstExaminationPattern/MstExaminationPatternAdd',
                data: $scope.examinationPattern,
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
                        $scope.examinationPattern = {};
                        $scope.getExaminationPattern();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Update ExaminationPattern*/
    $scope.editExaminationPattern = function () {

        if ($scope.examinationPattern.ExaminationPatternName === null || $scope.examinationPattern.ExaminationPatternName === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstExaminationPattern/MstExaminationPatternEdit',
                data: $scope.examinationPattern,
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
                        $scope.getExaminationPattern();
                        $scope.ShowFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete ExaminationPattern*/
    $scope.deleteExaminationPattern = function (ev,data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.examinationPattern = data;
                
                $http({
                    method: 'POST',
                    url: 'api/MstExaminationPattern/MstExaminationPatternDelete',
                    data: $scope.examinationPattern,
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
                            $scope.getExaminationPattern();
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

    /*Modify ExaminationPattern Data*/
    $scope.modifyExaminationPatternData = function (data) {
        $scope.ShowFormFlag = true;
        $scope.examinationPattern = data;
        $(window).scrollTop(0);
    };

    /*Add New ExaminationPattern*/
    $scope.newExaminationPatternAdd = function (data) {
        $state.go('examinationPatternAdd');
    };

    /*Back to Edit Page of ExaminationPattern*/
    $scope.backToList = function () {
        $state.go('examinationPatternEdit');
    };     

    /*Active Enable ExaminationPattern*/
    $scope.ShowExaminationPattern = function (data) {

        $scope.newExaminationPattern = data;
       
        $http({
            method: 'POST',
            url: 'api/MstExaminationPattern/MstExaminationPatternIsActive',
            data: $scope.newExaminationPattern,
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
                    $scope.getExaminationPattern();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable ExaminationPattern*/
    $scope.HideExaminationPattern = function (data) {

        $scope.newExaminationPattern = data;
             
        $http({
            method: 'POST',
            url: 'api/MstExaminationPattern/MstExaminationPatternIsSuspended',
            data: $scope.newExaminationPattern,
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
                    $scope.getExaminationPattern();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };   

});