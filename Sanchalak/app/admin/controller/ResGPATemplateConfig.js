app.controller('ResGPATemplateConfigCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res GPA Template Config";

    $scope.cardTitle = "Res GPA Template Config Operation";
    $scope.GTempConfig = {};

    $scope.getGradeLevelList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGradeLevels/ResGradeLevelsGet',
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
                        $scope.GradeLevelList = response.obj;
                      
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATemplateList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResGPATemplateGet',
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
                        $scope.GPATemplateAllList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATemplateByIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResGPATemplateGetById',
            data: { Id: $scope.GTempConfig.GradeTemplateId},
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
                        $scope.GPATemplateList = response.obj[0];
                      //  alert($scope.GPATemplateList.NoofIntervals);
                        $scope.Intervals = $scope.GPATemplateList.NoofIntervals;
                        $scope.ShowTable = true;
                        $scope.getGPATempConfigByGradeTempIdList();
                     
                            let GradeTempConfig = new Array();
                            for (var i = 0; i < $scope.Intervals; i++) {
                                var obj = {};
                                obj["GradeTemplateId"] = $scope.GTempConfig.GradeTemplateId;
                                obj["GradeAbbrev"] = "";
                                obj["MarkPercentRangeFrom"] = null;
                                obj["MarkPercentRangeTo"] = null;
                                obj["Status"] = "";
                                obj["GPAFrom"] = null;
                                obj["GPATo"] = null;
                                obj["GradePoint"] = null;
                                obj["GradeLevelId"] = 0;
                              
                                GradeTempConfig.push(obj);
                            }
                            $scope.List = GradeTempConfig;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATempConfigList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplateConfig/ResGPATemplateConfigGet',
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

                        $scope.GPATempConfigTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATempConfigByGradeTempIdList = function () {
     
        $http({
            method: 'POST',
            url: 'api/ResGPATemplateConfig/ResGPATemplateConfigGetByGradeTempId',
            data: { GradeTemplateId: $scope.GTempConfig.GradeTemplateId },
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
                       
                        if (response.obj != "")
                        $scope.List = response.obj;
                   
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addGPATempConfig = function () {
      
        $scope.GTempConfig.GTempConfigList = $scope.List;
        var countG = 0;
        var countM = 0;
        var validationFlag = false;
        for (var i = 0; i < $scope.List.length; i++) {
            if ($scope.List[i].GradeTemplateId == null || $scope.List[i].GradeTemplateId === undefined ||
                $scope.List[i].GradeAbbrev == null || $scope.List[i].GradeAbbrev === undefined || $scope.List[i].GradeAbbrev == 0.00 || $scope.List[i].GradeAbbrev == "" ||         
                $scope.List[i].Status == null || $scope.List[i].Status === undefined || $scope.List[i].Status == "" ||              
                $scope.List[i].GradePoint == null || $scope.List[i].GradePoint === undefined || $scope.List[i].GradePoint == 0 || $scope.List[i].GradePoint == "" ||
                $scope.List[i].GradeLevelId == null || $scope.List[i].GradeLevelId === undefined || $scope.List[i].GradeLevelId == 0
            ) {
                $scope.modifyUserFlag = true;
                validationFlag = false;
            }
            else {
                if(! (($scope.List[i].MarkPercentRangeFrom == null || $scope.List[i].MarkPercentRangeFrom === undefined ||
                    $scope.List[i].MarkPercentRangeTo == null || $scope.List[i].MarkPercentRangeTo === undefined || $scope.List[i].MarkPercentRangeTo == 0.00 ) &&
                    ($scope.List[i].GPAFrom == null || $scope.List[i].GPAFrom === undefined || 
                        $scope.List[i].GPATo == null || $scope.List[i].GPATo === undefined || $scope.List[i].GPATo == 0.00 || $scope.List[i].GPATo == "")))
                {
                    if ($scope.List[i].MarkPercentRangeFrom != null || $scope.List[i].MarkPercentRangeTo != null) {
                        if ($scope.List[i].MarkPercentRangeFrom < $scope.List[i].MarkPercentRangeTo) {
                            document.getElementById(i).innerHTML = "";
                            if ($scope.List[i].GPAFrom != null || $scope.List[i].GPATo != null) {
                                if ($scope.List[i].GPAFrom < $scope.List[i].GPATo) {
                                    document.getElementById(i + "_G").innerHTML = "";
                                    countM = countM + 1;
                                    if (countM == $scope.List.length) {
                                        validationFlag = true;
                                    }
                                    else { validationFlag = false; }
                                }
                                else {
                                    document.getElementById(i + "_G").innerHTML = "Grade From (%) should be less than Grade To (%)";
                                }
                            }
                            else {
                                countM = countM + 1;
                                if (countM == $scope.List.length) {
                                    validationFlag = true;
                                }
                                else { validationFlag = false; }
                            }

                        }
                        else {
                            document.getElementById(i).innerHTML = "Mark From (%) should be less than Mark To (%)";
                        }
                       
                    }
                    if ($scope.List[i].GPAFrom != null || $scope.List[i].GPATo != null) {
                        if ($scope.List[i].GPAFrom < $scope.List[i].GPATo) {
                            document.getElementById(i + "_G").innerHTML = "";
                            if ($scope.List[i].MarkPercentRangeFrom != null || $scope.List[i].MarkPercentRangeTo != null)
                            {
                                if ($scope.List[i].MarkPercentRangeFrom < $scope.List[i].MarkPercentRangeTo)
                                {
                                    document.getElementById(i).innerHTML = "";
                                    countG = countG + 1;
                                    if (countG == $scope.List.length) {
                                        validationFlag = true;
                                    }
                                    else { validationFlag = false; }
                                }
                                else
                                { document.getElementById(i).innerHTML = "Mark From (%) should be less than Mark To (%)"; }
                            }
                            else { 
                            countG = countG + 1;
                            if (countG == $scope.List.length) {
                                validationFlag = true;
                            }
                            else { validationFlag = false; }
                        }
                        }
                        else {
                            document.getElementById(i + "_G").innerHTML = "Grade From (%) should be less than Grade To (%)";
                        }
                    }
                  
                   
                }
                else {
                    alert("Please Fill Grade or marks details");
                    exit();
                    }
                    
                }        

        }
        if (validationFlag == true) {
        $http({
            method: 'POST',
            url: 'api/ResGPATemplateConfig/ResGPATemplateConfigAdd',
            data: $scope.GTempConfig,
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
                        $scope.GTempConfig = {};
                       // $scope.getGPATempConfigList();
                        $scope.ShowTable = false;
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

    $scope.modifyGPATempConfigData = function (data) {
        $scope.showFormFlag = true;
        $scope.GTempConfig = data;
        $scope.getGPATempConfigList();
    };

    $scope.editGPATempConfig = function () {

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
            url: 'api/ResGPATemplateConfig/ResGPATemplateConfigEdit',
            data: $scope.GTempConfig,
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
                        $scope.GTempConfig = {};
                        $scope.getGPATempConfigList();
                        $scope.showFormFlag = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        //  }
    };

    $scope.deleteGPATempConfig = function (ev) {
        if ($scope.List == null || $scope.List === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResCalculation')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("There is No Record")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var confirm = $mdDialog.confirm()
                .title('Would you like to delete?')
                .textContent('Record will be deleted permanently.')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {

                $scope.GTempConfig.GTempConfigList = $scope.List;
                // $scope.GTempConfig = data;
                $http({
                    method: 'POST',
                    url: 'api/ResGPATemplateConfig/ResGPATemplateConfigDelete',
                    data: $scope.GTempConfig,
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
                                $scope.GTempConfig = {};
                                $scope.ShowTable = false;
                                //$scope.getGPATempConfigList();

                            }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });

            },
                function () {
                    $scope.status = 'You decided to keep your debt.';
                });
        }
    };

    $scope.newGPATempConfigAdd = function () {
        $state.go('ResGPATemplateConfigAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResGPATemplateConfigEdit');
    };
    $scope.backToList = function () {
        $state.go('ResGPATemplateConfigEdit');
    };
    $scope.displayResMarksLevel = function (data) {
        $scope.GTempConfig = data;
    };
    $scope.cancelGPATempConfig = function () {
        $scope.GTempConfig = {};
        $scope.modifyUserFlag = false;
        $scope.ShowTable = false;
    }

    $scope.modifyGPATempConfig = function (data) {
        $scope.GTempConfig = data;
        $scope.modifyUserFlag = true;
    }

});