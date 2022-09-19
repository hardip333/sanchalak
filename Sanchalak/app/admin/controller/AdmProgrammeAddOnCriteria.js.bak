app.controller('AdmProgrammeAddOnCriteriaCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Adm Programme AddOn Criteria";
   


    $scope.ProgrammeAddOnTableParams = new NgTableParams({
    }, {
            dataset: $scope.ProgrammeAddOnList
    });

    $scope.resetProgrammeAddOn = function () {
        $scope.ProgrammeAddOn = {};
    };

    $scope.getProgrammeAddOnListById = function () {
        $scope.ProgrammeInstanceName = $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;
        $http({
            method: 'Post',
            url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaGetById',
            data: { ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //$scope.ProgrammeAddOnList = response.obj;
               $scope.ProgrammeAddOnTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammeAddOnList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaGetAll',
            data: data,
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
                    $scope.ProgrammeAddOnAllList = response.obj;
                    //$scope.ProgrammeAddOnTableParams = new NgTableParams({
                    //}, {
                    //    dataset: response.obj
                    //});
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
   
    $scope.addProgrammeAddOn = function () {

        if ($scope.ProgrammeAddOn.TitleName === null || $scope.ProgrammeAddOn.TitleName === undefined ||
            $scope.ProgrammeAddOn.TitleType === null || $scope.ProgrammeAddOn.TitleType === undefined) {

            alert("please check all fields !!!");
        }
        else {
            $scope.ProgrammeAddOn.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
            $http({
                method: 'POST',
                url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaAdd',
                data: $scope.ProgrammeAddOn,
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
                        $scope.ProgrammeAddOn = {};
                        $scope.getProgrammeAddOnListById();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.cancelProgrammeAddOn = function () {
        $scope.ProgrammeAddOn = {};
        $scope.modifyProgrammeAddOnFlag = false;
    };

    $scope.modifyMstProgrammeAddOnData = function (data) {
        $scope.showFormFlag = true;
        $scope.ProgrammeAddOn = data;
        $scope.getProgrammeAddOnListById();
    };

    $scope.displayProgrammeAddOn = function (data) {
        $scope.ProgrammeAddOn = data;
    };

    $scope.modifyProgrammeAddOn = function () {

        if ($scope.ProgrammeAddOn.TitleName === null || $scope.ProgrammeAddOn.TitleName === undefined ||
            $scope.ProgrammeAddOn.TitleType === null || $scope.ProgrammeAddOn.TitleType === undefined) {

            alert("please check all fields !!!");
        }
        else {
            $scope.ProgrammeAddOn.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
            $http({
                method: 'POST',
                url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaEdit',
                data: $scope.ProgrammeAddOn,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert("your data updated successfully");
                        $scope.ProgrammeAddOn = {};
                        $scope.getProgrammeAddOnListById();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteProgrammeAddOn = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgrammeAddOn = data;

            $http({
                method: 'POST',
                url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaDelete',
                data: $scope.ProgrammeAddOn,
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
                        alert("your data deleted successfully");
                        $scope.getProgrammeAddOnListById();
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

    $scope.ShowBos = function (data) {

        $scope.newProgrammeAddOn = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyIsActive',
            data: $scope.newProgrammeAddOn,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert("Record Activated !!!");
                    $scope.getProgrammeAddOnList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideBos = function (data) {

        $scope.newProgrammeAddOn = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyIsSuspended',
            data: $scope.newProgrammeAddOn,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert("Record suspended !!!");
                    $scope.getProgrammeAddOnList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.nextAdd = function () {
        $state.go('AdmRequiredDocumentsProgramEdit');
    };

    $scope.newProgrammeAddOnAdd = function () {
        $state.go('AdmProgrammeAddOnCriteriaAdd');
    };

    $scope.backToList = function () {
        $state.go('AdmProgrammeAddOnCriteriaEdit');
    };

});