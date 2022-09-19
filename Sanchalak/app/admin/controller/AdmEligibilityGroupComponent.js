﻿app.controller('AdmEligibilityGroupComponentCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Adm Eligibility Group Component";



    $scope.GroupComponentTableParams = new NgTableParams({
    }, {
        dataset: $scope.GroupComponentList
    });

    $scope.resetGroupComponent = function () {
        $scope.GroupComponent = {};
    };

    $scope.getDegree = function () {
        //alert("Show List");
        $http({
            method: 'Post',
            url: 'api/AdmEligibleDegree/getAdmEligibleDegreeList',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.DegreeList = response.obj;

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
                $scope.SpecializationList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getEligibilityGroupListById = function () {
    
        $http({
            method: 'POST',
            url: 'api/AdmEligibilityGroup/AdmEligibilityGroupGetById',
            data: { ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm },
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
                        $scope.GroupList = response.obj;
                        //  console.log($scope.GroupList);
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.getCriteria = function () {
       
        $http({
            method: 'Post',
            url: 'api/AdmEligibilityGroup/AdmEligibilityGroupGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.CriteriaList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getGroupComponentListById = function () {
       
        $scope.ProgrammeInstanceName = $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;
        $http({
            method: 'Post',
            url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentGetById',
            data: { ProgramInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //$scope.GroupComponentList = response.obj;
                $scope.GroupComponentTableParams = new NgTableParams({
                }, {
                    dataset: response.obj
                });
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getGroupComponentList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentGetAll',
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
                    $scope.GroupCompList = response.obj;
                    //$scope.GroupComponentTableParams = new NgTableParams({
                    //}, {
                    //    dataset: response.obj
                    //});
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addGroupComponent = function () {
      
        if ($scope.GroupComponent.EligibilityGroupId === null || $scope.GroupComponent.EligibilityGroupId === undefined ||
            $scope.GroupComponent.EligibleDegreeId === null || $scope.GroupComponent.EligibleDegreeId === undefined ||
            $scope.GroupComponent.EligibilitySpecializationId === null || $scope.GroupComponent.EligibilitySpecializationId === undefined //||
            //$scope.GroupComponent.MinimumPercentage === null || $scope.GroupComponent.MinimumPercentage === undefined || 
            //$scope.GroupComponent.GradeCGPA === null || $scope.GroupComponent.GradeCGPA === undefined 
        ) {

            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GroupComponentAdd')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Add...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.GroupComponent.ProgramInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
      
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentAdd',
                data: $scope.GroupComponent,
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
                        $scope.GroupComponent = {};
                        $scope.getGroupComponentListById();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.cancelGroupComponent = function () {
        $scope.GroupComponent = {};
        $scope.modifyGroupComponentFlag = false;
    };

    $scope.modifyMstGroupComponentData = function (data) {
        $scope.showFormFlag = true;
        $scope.GroupComponent = data;
        $scope.getEligibilityGroupListById();
        $scope.getCriteria();
        $scope.getSpecialization();
        $scope.getDegree();
    };

    $scope.displayGroupComponent = function (data) {
        $scope.GroupComponent = data;
    };

    $scope.modifyGroupComponent = function () {

        if ($scope.GroupComponent.EligibilityGroupId === null || $scope.GroupComponent.EligibilityGroupId === undefined ||
            $scope.GroupComponent.EligibleDegreeId === null || $scope.GroupComponent.EligibleDegreeId === undefined ||
            $scope.GroupComponent.EligibilitySpecializationId === null || $scope.GroupComponent.EligibilitySpecializationId === undefined// ||
            //$scope.GroupComponent.MinimumPercentage === null || $scope.GroupComponent.MinimumPercentage === undefined ||
            //$scope.GroupComponent.GradeCGPA === null || $scope.GroupComponent.GradeCGPA === undefined
        ) {

            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GroupComponentEdit')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Update...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.GroupComponent.ProgramInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentEdit',
                data: $scope.GroupComponent,
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
                        $scope.GroupComponent = {};
                        $scope.getGroupComponentListById();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    $scope.deleteGroupComponent = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            //$scope.newUser.createdById = $rootScope.id;
            $scope.GroupComponent = data;
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityCriteriaComponent/AdmEligibilityCriteriaComponentDelete',
                data: $scope.GroupComponent,
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
                            $scope.GroupComponent = {};                      
                            $scope.getGroupComponentListById();

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

    $scope.ShowGroupComponent = function (data) {

        $scope.newGroupComponent = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyIsActive',
            data: $scope.newGroupComponent,
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
                    $scope.getGroupComponentList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideGroupComponent = function (data) {

        $scope.newGroupComponent = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyIsSuspended',
            data: $scope.newGroupComponent,
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
                    $scope.getGroupComponentList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.nextAdd = function () {
        $state.go('AdmProgrammeAddOnCriteriaEdit');
    };
    $scope.newGroupComponentAdd = function () {
        $state.go('AdmEligibilityGroupComponentAdd');
    };

    $scope.backToList = function () {
        $state.go('AdmEligibilityGroupComponentEdit');
    };

});