app.controller('AdmSpecializationMasterCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage AdmSpecializationMaster";

    /*Reset AppearanceType*/
    $scope.resetAdmSpecializationMaster = function () {
        $scope.AdmSpecializationMaster = {};
    };

    /*Get AdmSpecializationMaster List*/
    $scope.getAdmSpecializationMaster = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/AdmSpecializationMaster/AdmSpecializationMasterGet',
            
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
                    $scope.AdmSpecializationMasterTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.getAdmEligibleDegreeList = function () {
        $http({
            method: 'POST',
            url: 'api/AdmSpecializationMaster/AdmEligibleDegreeGet',
            data: $scope.AdmSpecializationMaster,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.EligibleDegreeList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add AdmSpecializationMaster*/
    $scope.addAdmSpecializationMaster = function () {

        if ($scope.AdmSpecializationMaster.SpecializationCode === null || $scope.AdmSpecializationMaster.SpecializationName === undefined ||
            $scope.AdmSpecializationMaster.SpecializationName === null || $scope.AdmSpecializationMaster.SpecializationName === undefined ||
            $scope.AdmSpecializationMaster.EligibleDegreeId === null || $scope.AdmSpecializationMaster.EligibleDegreeId === undefined) {
            alert("Enter Specialization Code & Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/AdmSpecializationMaster/AdmSpecializationMasterAdd',
                data: $scope.AdmSpecializationMaster,
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
                        $scope.AdmSpecializationMaster = {};
                        $scope.getAdmSpecializationMaster();
                        $state.go('AdmSpecializationMasterEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify AdmSpecializationMaster Data*/
    $scope.modifyAdmSpecializationMasterData = function (data) {
        $scope.AdmSpecializationMaster = data;
        $scope.getAdmEligibleDegreeList();
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update AdmSpecializationMaster*/
    $scope.editAdmSpecializationMaster = function () {
        if ($scope.AdmSpecializationMaster.SpecializationCode === null || $scope.AdmSpecializationMaster.SpecializationName === undefined ||
            $scope.AdmSpecializationMaster.SpecializationName === null || $scope.AdmSpecializationMaster.SpecializationName === undefined ||
            $scope.AdmSpecializationMaster.EligibleDegreeId === null || $scope.AdmSpecializationMaster.EligibleDegreeId === undefined) {
            alert("Enter Specialization Code & Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/AdmSpecializationMaster/AdmSpecializationMasterUpdate',
                data: $scope.AdmSpecializationMaster,
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
                        $scope.getAdmSpecializationMaster();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete AdmSpecializationMaster*/
    $scope.deleteAdmSpecializationMaster = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.AdmSpecializationMaster = data;

            $http({
                method: 'POST',
                url: 'api/AdmSpecializationMaster/AdmSpecializationMasterDelete',
                data: $scope.AdmSpecializationMaster,
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
                        $scope.getAdmSpecializationMaster();
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

    /*Add New AdmSpecializationMaster*/
    $scope.newAdmSpecializationMasterAdd = function () {
        $state.go('AdmSpecializationMasterAdd');
    };

    /*Back to Edit Page of AdmSpecializationMaster*/
    $scope.backToList = function () {
        $state.go('AdmSpecializationMasterEdit');
    };

    /*Display AdmSpecializationMaster Data*/
    $scope.displayAdmSpecializationMaster = function (data) {
        $scope.AdmSpecializationMaster = data;
    };

    

});






    

