app.controller('AdmEligibilityGroupCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Adm Eligibility Group";

    $scope.cardTitle = "Adm Eligibility Group Operation";


    //$scope.rdocumentTableParams = new NgTableParams({
    //}, {
    //        dataset: $scope.rdocument
    //});

    $scope.resetEligibilityGroup = function () {
        $scope.EligibilityGroup = {};
    }

    $scope.getEligibilityGroupList = function () {
        //alert("Document");
        var data = new Object();
        //data.id = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/AdmEligibilityGroup/AdmEligibilityGroupGet',
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
                        $scope.GroupAllList = response.obj;
                        //$scope.EligibilityGroupTableParams = new NgTableParams({
                        //}, {
                        //    dataset: response.obj
                        //});
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
   // $scope.getEligibilityGroupList();

    $scope.getEligibilityGroupListById = function () {
      
        $scope.ProgrammeInstanceName =  $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;
        $http({
            method: 'POST',
            url: 'api/AdmEligibilityGroup/AdmEligibilityGroupGetById',
            data: { ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm},
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
                        //$scope.GroupList = response.obj;
                        $scope.EligibilityGroupTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.addEligibilityGroup = function () {
        //alert("Add Document");
        //alert("Program Instance Part Term local storage value" + $localStorage.ProgrammeInstancePartTerm);
        if ($scope.EligibilityGroup.EligibilityGroup == null || $scope.EligibilityGroup.EligibilityGroup === undefined            
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Add...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityGroup/AdmEligibilityGroupAdd',
                data: {
                    EligibilityGroup: $scope.EligibilityGroup.EligibilityGroup,
                    ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm
                },
                    //$scope.EligibilityGroup,
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
                            $scope.EligibilityGroup = {};
                            //$scope.EligibilityGroup.ProgrammeInstancePartTermId = $localStorage.ProgrammeInstancePartTerm;                           
                            $scope.getEligibilityGroupListById();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.modifyMstEligibilityGroupData = function (data) {
        $scope.showFormFlag = true;
        $scope.EligibilityGroup = data;
        $scope.getEligibilityGroupList();
    };

    $scope.editEligibilityGroup = function () {
        $scope.EligibilityGroup.EligibilityGroup = $scope.EligibilityGroup.EligibilityGroup;
        $scope.EligibilityGroup.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
        //$scope.newUser.createdById = $rootScope.id;
        if ($scope.EligibilityGroup.EligibilityGroup == null || $scope.EligibilityGroup.EligibilityGroup === undefined ||
            $scope.EligibilityGroup.ProgrammeInstancePartTermId == null || $scope.EligibilityGroup.ProgrammeInstancePartTermId === undefined           
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Edit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityGroup/AdmEligibilityGroupEdit',
                data: $scope.EligibilityGroup,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //  $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.EligibilityGroup = {};
                           // $scope.EligibilityGroup.ProgrammeInstancePartTermId = $localStorage.ProgrammeInstancePartTerm;
                            $scope.getEligibilityGroupListById();
                            $scope.showFormFlag = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteEligibilityGroup = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            //$scope.newUser.createdById = $rootScope.id;
            $scope.EligibilityGroup = data;
            $http({
                method: 'POST',
                url: 'api/AdmEligibilityGroup/AdmEligibilityGroupDelete',
                data: $scope.EligibilityGroup,
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
                            $scope.EligibilityGroup = {};
                           // $scope.EligibilityGroup.ProgrammeInstancePartTermId = $localStorage.ProgrammeInstancePartTerm;
                            $scope.getEligibilityGroupListById();

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

    $scope.newEligibilityGroupAdd = function () {
        $state.go('AdmEligibilityGroupAdd');
    };
    $scope.nextAdd = function () {
        $state.go('AdmEligibilityGroupComponentEdit');
    };
    $scope.backToList = function () {
        $state.go('AdmEligibilityGroupEdit');
    };  
    $scope.displayEligibilityGroup = function (data) {
        $scope.EligibilityGroup = data;
    };
    $scope.cancelEligibilityGroup = function () {
        $scope.EligibilityGroup = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyEligibilityGroup = function (data) {
        $scope.EligibilityGroup = data;
        $scope.modifyUserFlag = true;
    }
    
});