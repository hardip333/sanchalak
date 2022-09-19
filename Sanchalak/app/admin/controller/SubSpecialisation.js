app.controller('SubSpecialisationCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    
    $rootScope.pageTitle = "Manage SubSpecialisation";

    /*Reset SubSpecialisation*/
    $scope.resetSubSpecialisation = function () {
        $scope.SubSpecialisation = {};
    };

    /*Get SubSpecialisation List*/
    $scope.getSubSpecialisation = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/SubSpecialisation/SubSpecialisationGet',
            
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
                    $scope.SubSpecialisationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Specialisation List*/
    $scope.SpecialisationGet = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstSpecialisation/MstSpecialisationGet',
            //data: SubSpecialisation,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add SubSpecialisation*/
    $scope.addSubSpecialisation = function () {
        if ($scope.SubSpecialisation.SpecialisationId === null || $scope.SubSpecialisation.SpecialisationId === undefined ||
            $scope.SubSpecialisation.SubSpecialisationName === null || $scope.SubSpecialisation.SubSpecialisationName === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/SubSpecialisation/SubSpecialisationAdd',
                data: $scope.SubSpecialisation,
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
                        $scope.SubSpecialisation = {};
                        $scope.getSubSpecialisation();
                        $state.go('UpdateSubSpecialisation');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    };

    /*Modify SubSpecialisation Data*/
    $scope.modifySubSpecialisationData = function (data) {

        $scope.showFormFlag = true;
        $scope.SubSpecialisation = data;
        $(window).scrollTop(0);
    };

    /*Update SubSpecialisation*/ 
    $scope.editSubSpecialisation = function () {
        if ($scope.SubSpecialisation.SpecialisationId === null || $scope.SubSpecialisation.SpecialisationId === undefined ||
            $scope.SubSpecialisation.SubSpecialisationName === null || $scope.SubSpecialisation.SubSpecialisationName === undefined) { alert("Enter All Fields"); }
        else {
            $http({
                method: 'POST',
                url: 'api/SubSpecialisation/SubSpecialisationUpdate',
                data: $scope.SubSpecialisation,
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
                        $scope.getSubSpecialisation();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
        
    };

    /*Delete SubSpecialisation*/
    $scope.deleteSubSpecialisation = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.SubSpecialisation = data;

            $http({
                method: 'POST',
                url: 'api/SubSpecialisation/SubSpecialisationDelete',
                data: $scope.SubSpecialisation,
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
                        $scope.getSubSpecialisation();
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

    /*Add New SubSpecialisation*/
    $scope.newSubSpecialisationAdd = function () {
        $state.go('SubSpecialisationAdd');
    };

    /*Back to Edit Page of SubSpecialisation*/
    $scope.backToList = function () {
        $state.go('SubSpecialisationEdit');
    };

    /*Display SubSpecialisation Data*/
    $scope.displaySubSpecialisation = function (data) {
        $scope.SubSpecialisation = data;
    };

    /*Active Enable SubSpecialisation*/
    $scope.ShowSubSpecialisation = function (data) {

        $scope.newSubSpecialisation = data;

        $http({
            method: 'POST',
            url: 'api/SubSpecialisation/SubSpecialisationIsActive',
            data: $scope.newSubSpecialisation,
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
                    $scope.getSubSpecialisation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable SubSpecialisation*/
    $scope.HideSubSpecialisation = function (data) {
        
        $scope.newSubSpecialisation = data;

        $http({
            method: 'POST',
            url: 'api/SubSpecialisation/SubSpecialisationIsSuspended',
            data: $scope.newSubSpecialisation,
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
                    $scope.getSubSpecialisation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});