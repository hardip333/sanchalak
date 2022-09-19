app.controller('AdmEligibilityCriteriaComponentCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Adm Eligibility Criteria Component";

    $scope.cardTitle = "Adm Eligibility Criteria Component Operation";


    //$scope.AdmEligibilityCriteriaComponentTableParams = new NgTableParams({
    //}, {
    //    dataset: $scope.userList
    //});

    //$scope.resetAdmEligibilityCriteriaComponent = function () {
    //    $scope.AdmCriteria = {};
    //}
    $scope.getDegree = function () {
        //alert("Show List");
        $http({
            method: 'Post',
            url: 'api/AdmEligibleDegree/getAdmEligibleDegreeList',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c1List = response.obj;
                $scope.table1 = {
                };

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getSpecialization = function () {
        //alert("Show List");
        $http({
            method: 'Post',
            url: 'api/AdmEligibilitySpecialization/AdmEligibilitySpecializationGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c2List = response.obj;
                $scope.table2 = {
                };

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getCriteria = function () {
        //alert("Show List");
        $http({
            method: 'Post',
            url: 'api/AdmEligibilityGroup/AdmEligibilityGroupGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c3List = response.obj;
                $scope.table3 = {
                };

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAdmEligibilityCriteriaComponentList = function () {
        //alert("Document");
        var data = new Object();
        //data.id = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentGet1',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.AdmEligibilityCriteriaComponentTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    $scope.getAdmEligibilityCriteriaComponentList();

    $scope.addAdmEligibilityCriteriaComponent = function () {
        alert("Add Document");
        //$scope.newUser.createdById = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentAdd',
            data: $scope.AdmCriteria,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.AdmCriteria = {};
                        $scope.getAdmEligibilityCriteriaComponentList();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    $scope.editAdmEligibilityCriteriaComponent = function () {

        //$scope.newUser.createdById = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentEdit',
            data: $scope.AdmCriteria,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.AdmCriteria = {};
                        $scope.getAdmEligibilityCriteriaComponentList();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }


    $scope.deleteAdmEligibilityCriteriaComponent = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {
            $scope.AdmCriteria = data;
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentDelete',
                data: $scope.AdmCriteria,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.AdmCriteria = {};
                            $scope.getAdmEligibilityCriteriaComponentList();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        },
            function () {
                $scope.status = 'You decided to keep your debt.';
            });
    };


    $scope.cancelAdmEligibilityCriteriaComponent = function () {
        $scope.AdmCriteria = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyAdmEligibilityCriteriaComponent = function (data) {
        $scope.AdmCriteria = data;
        $scope.modifyUserFlag = true;
    }

    $scope.modifyUser = function () {

        $scope.newUser.createdById = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/Admin/updateAdmin',
            data: $scope.newUser,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.newUser = {};
                    $scope.getUserList();
                    $scope.modifyUserFlag = false;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.showUser = function (data) {

        data.createdById = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/Admin/showAdmin',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getUserList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.hideUser = function (data) {

        data.createdById = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/Admin/hideAdmin',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getUserList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});
