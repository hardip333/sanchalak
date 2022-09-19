app.controller('ResMarksTemplateConfigCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res Marks Template Config";

    $scope.cardTitle = "Res Marks Template Config Operation";

    $scope.getResMarksLevelList = function () {

        $http({
            method: 'POST',
            url: 'api/ResMarksLevels/ResMarksLevelsGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.MarksLevelList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMarksTemplateList = function () {

        $http({
            method: 'POST',
            url: 'api/ResMarksTemplate/ResMarksTemplateGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.MarksTempList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMarksTemplateByIdList = function () {
        
        $http({
            method: 'POST',
            url: 'api/ResMarksTemplate/ResMarksTemplateGetById',
            data: { Id: $scope.TempConfig.MarkTemplateId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.MarksTempByIdList = response.obj[0];
                        $scope.Intervals = $scope.MarksTempByIdList.NoofIntervals;
                        $scope.ShowTable = true;
                        $scope.getMarksTempConfigListById();
                     
                            //alert($scope.Intervals);
                            let MarksTempConfig = new Array();
                            for (var i = 0; i < $scope.Intervals; i++) {
                                var obj = {};
                                obj["ClassId"] = 0;
                                obj["RangeTo"] = null;
                                obj["RangeFrom"] = null;
                                obj["MarkTemplateId"] = $scope.TempConfig.MarkTemplateId;
                                MarksTempConfig.push(obj);
                            }
                            $scope.List = MarksTempConfig;
                        
                     
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMarksTempConfigList = function () {
      
        $http({
            method: 'POST',
            url: 'api/ResMarksTemplateConfig/ResMarksTemplateConfigGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.TempConfigTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMarksTempConfigListById = function () {

        $http({
            method: 'POST',
            url: 'api/ResMarksTemplateConfig/ResMarksTemplateConfigGetById',
            data: { MarkTemplateId: $scope.TempConfig.MarkTemplateId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        if (response.obj != "") { 
                       
                            $scope.List = response.obj;
                        }
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addMarksTempConfig = function () {
      
        $scope.TempConfig.List1 = $scope.List;
        var count = 0;
        var validationFlag = false;
        for (var i = 0; i < $scope.List.length; i++) {
            if ($scope.List[i].ClassId == null || $scope.List[i].ClassId === undefined || $scope.List[i].ClassId == 0 ||
                $scope.List[i].RangeFrom == null || $scope.List[i].RangeFrom === undefined || //$scope.List[i].RangeFrom == "" ||
                $scope.List[i].RangeTo == null || $scope.List[i].RangeTo === undefined || $scope.List[i].RangeTo == "" ||
                $scope.List[i].MarkTemplateId == null || $scope.List[i].MarkTemplateId === undefined
            ) {
                $scope.modifyUserFlag = true;
                validationFlag = false;
            }

            else {
                if ($scope.List[i].RangeFrom < $scope.List[i].RangeTo) {
                    document.getElementById(i).innerHTML = "";
                    count = count + 1;
                    if (count == $scope.List.length) {
                        validationFlag = true;
                    }
                    else { validationFlag = false; }
                    
                }
                else {
                    document.getElementById(i).innerHTML = "From (%) should be less than To (%)";
                }
            }
        }
        if (validationFlag == true) {
            $http({
                method: 'POST',
                url: 'api/ResMarksTemplateConfig/ResMarksTemplateConfigAdd',
                data: $scope.TempConfig,
                headers: { "Content-Type": 'application/json' }

            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.ShowTable = false;
                            $scope.TempConfig = {};
                            $scope.getMarksTempConfigListById();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#MarksTemplateConfig')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Save...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
    };

    $scope.modifyMarksTempConfigData = function (data) {
        $scope.showFormFlag = true;
        $scope.TempConfig = data;
        $scope.getMarksTempConfigList();
    };

    $scope.editMarksTempConfig = function () {

        //if ($scope.EligibilityGroup.EligibilityGroup == null || $scope.EligibilityGroup.EligibilityGroup === undefined ||
        //    $scope.EligibilityGroup.ProgrammeInstancePartTermId == null || $scope.EligibilityGroup.ProgrammeInstancePartTermId === undefined
        //) {
        //    $scope.modifyUserFlag = true;
        //    $mdDialog.show(
        //        $mdDialog.alert()
        //            .parent(angular.element(document.querySelector('#EligibilityGroupform')))
        //            .clickOutsideToClose(true)
        //            .title("Error")
        //            .textContent("Please Fill all Mandatory details before Edit...")
        //            .ariaLabel('Alert Dialog Demo')
        //            .ok('Okay!')
        //    );
        //}
        //else {
        $http({
            method: 'POST',
            url: 'api/ResMarksTemplateConfig/ResMarksTemplateConfigEdit',
            data: $scope.TempConfig,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //  $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.TempConfig = {};
                        $scope.getMarksTempConfigList();
                        $scope.showFormFlag = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        //  }
    };

    $scope.deleteMarksTempConfig = function (ev) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {
            $scope.TempConfig.List1 = $scope.List;
            //$scope.TempConfig = data;
            $http({
                method: 'POST',
                url: 'api/ResMarksTemplateConfig/ResMarksTemplateConfigDelete',
                data: $scope.TempConfig,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.TempConfig = {};
                           // $scope.getMarksTempConfigList();
                            $scope.ShowTable = false;

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

    $scope.newMarksTempConfigAdd = function () {
        $state.go('ResMarksTemplateAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResMarksTemplateEdit');
    };
    $scope.backToList = function () {
        $state.go('ResMarksTemplateEdit');
    };
    $scope.displayMarksTempConfig = function (data) {
        $scope.TempConfig = data;
    };
    $scope.cancelMarksTempConfig = function () {
        $scope.TempConfig = {};
        $scope.modifyUserFlag = false;
        $scope.ShowTable = false;
    }

    $scope.modifyMarksTempConfig = function (data) {
        $scope.TempConfig = data;
        $scope.modifyUserFlag = true;
    }

});