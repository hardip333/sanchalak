app.controller('EvaluationCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage Evaluation";

    /*Reset Evaluation*/
    $scope.resetEvaluation = function () {
        $scope.Evaluation = {};
    };

    /*Get Evaluation List*/
    $scope.getEvaluation = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstEvaluation/EvaluationGet',
            
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
                    $scope.EvaluationTableParams = new NgTableParams({
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
    $scope.addEvaluation = function () {

        if ($scope.Evaluation.EvaluationName === null || $scope.Evaluation.EvaluationName === undefined) {
            alert("Enter Evaluation Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstEvaluation/EvaluationAdd',
                data: $scope.Evaluation,
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
                        $scope.Evaluation = {};
                        $scope.getEvaluation();
                        $state.go('UpdateEvaluation');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify Evaluation Data*/
    $scope.modifyEvaluationData = function (data) {
        $scope.Evaluation = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update Evaluation*/
    $scope.editEvaluation = function () {
        if ($scope.Evaluation.EvaluationName === null || $scope.Evaluation.EvaluationName === undefined) {
            alert("Enter Evaluation Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstEvaluation/EvaluationUpdate',
                data: $scope.Evaluation,
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
                        $scope.getEvaluation();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Evaluation*/
    $scope.deleteEvaluation = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.Evaluation = data;

            $http({
                method: 'POST',
                url: 'api/MstEvaluation/EvaluationDelete',
                data: $scope.Evaluation,
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
                        $scope.getEvaluation();
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
    $scope.newEvaluationAdd = function () {
        $state.go('EvaluationAdd');
    };

    /*Back to Edit Page of Evaluation*/
    $scope.backToList = function () {
        $state.go('EvaluationEdit');
    };

    /*Display Evaluation Data*/
    $scope.displayEvaluation = function (data) {
        $scope.Evaluation = data;
    };

    /*Active Enable Evaluation*/
    $scope.ShowEvaluation = function (data) {

        $scope.Evaluation = data;

        $http({
            method: 'POST',
            url: 'api/MstEvaluation/EvaluationIsActive',
            data: $scope.Evaluation,
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
                    $scope.getEvaluation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Evaluation*/
    $scope.HideEvaluation = function (data) {

        $scope.Evaluation = data;

        $http({
            method: 'POST',
            url: 'api/MstEvaluation/EvaluationIsSuspended',
            data: $scope.Evaluation,
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
                    $scope.getEvaluation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

