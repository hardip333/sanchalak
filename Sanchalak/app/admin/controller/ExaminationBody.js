app.controller('ExaminationBodyCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage ExaminationBody";

    /*Reset AppearanceType*/
    $scope.resetExaminationBody = function () {
        $scope.ExaminationBody = {};
    };

    /*Get ExaminationBody List*/
    $scope.getExaminationBody = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ExaminationBody/ExaminationBodyGet',
            
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
                    $scope.ExaminationBodyTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add ExaminationBody*/
    $scope.addExaminationBody = function () {

        if ($scope.ExaminationBody.ExaminationBodyCode === null || $scope.ExaminationBody.ExaminationBodyCode === undefined ||
            
            $scope.ExaminationBody.ExaminationBodyName === null || $scope.ExaminationBody.ExaminationBodyName === undefined )
        {
            alert("Enter ExaminationBody Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/ExaminationBody/ExaminationBodyAdd',
                data: $scope.ExaminationBody,
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
                        $scope.ExaminationBody = {};
                        $scope.getExaminationBody();
                        $state.go('ExaminationBodyEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify ExaminationBody Data*/
    $scope.modifyExaminationBodyData = function (data) {
        $scope.ExaminationBody = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update ExaminationBody*/
    $scope.editExaminationBody = function () {
        if ($scope.ExaminationBody.ExaminationBodyCode === null || $scope.ExaminationBody.ExaminationBodyCode === undefined ||

            $scope.ExaminationBody.ExaminationBodyName === null || $scope.ExaminationBody.ExaminationBodyName === undefined)
        {
            alert("Enter ExaminationBody Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/ExaminationBody/ExaminationBodyUpdate',
                data: $scope.ExaminationBody,
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
                        $scope.getExaminationBody();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete ExaminationBody*/
    $scope.deleteExaminationBody = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ExaminationBody = data;

            $http({
                method: 'POST',
                url: 'api/ExaminationBody/ExaminationBodyDelete',
                data: $scope.ExaminationBody,
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
                        $scope.getExaminationBody();
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

    /*Add New AppearanceType*/
    $scope.newExaminationBodyAdd = function () {
        $state.go('ExaminationBodyAdd');
    };

    /*Back to Edit Page of ExaminationBody*/
    $scope.backToList = function () {
        $state.go('ExaminationBodyEdit');
    };

    /*Display ExaminationBody Data*/
    $scope.displayExaminationBody = function (data) {
        $scope.ExaminationBody = data;
    };

    /*Active Enable ExaminationBody*/
    $scope.ShowExaminationBody = function (data) {

        $scope.ExaminationBody = data;

        $http({
            method: 'POST',
            url: 'api/ExaminationBody/ExaminationBodyIsActive',
            data: $scope.ExaminationBody,
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
                    $scope.getExaminationBody();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable ExaminationBody*/
    $scope.HideExaminationBody = function (data) {

        $scope.ExaminationBody = data;

        $http({
            method: 'POST',
            url: 'api/ExaminationBody/ExaminationBodyIsSuspended',
            data: $scope.ExaminationBody,
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
                    $scope.getExaminationBody();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

