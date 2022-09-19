
app.controller('ResBasisForAwardingCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Result Basis For Awarding";

    $scope.cardTitle = "Result Basis For Awarding Operation";

    $scope.ResBForAwarding = {};

    $scope.getResMstFaculty = function () {

        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResMstFacultyGet',
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
                        $scope.FacultyList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResMstProgBranchMapByProgInstId = function () {

        $http({
            method: 'POST',
            url: 'api/ResBasisForAwarding/ResMstProgBranchMapByProgInsId',
            data: {
                ProgramInstanceId: $scope.ResBForAwarding.ProgramInstanceId              
            },
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

                        $scope.ProgBranchList = response.obj;
                    
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResMstEvaluation = function () {
    
        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResMstEvaluationGet',
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
                        $scope.EvaluationList = response.obj;                     
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
            data: { Id: $scope.ResBForAwarding.MarkTemplateId },
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
                        $scope.GPATemplateByIdList = {};
                        $scope.MarksTempByIdList = response.obj[0];
                        $scope.getMarksTempConfigListById();
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
            data: { MarkTemplateId: $scope.ResBForAwarding.MarkTemplateId },
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

                            $scope.MarksTempConfigList = response.obj;
                        }
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGradeScaleList = function () {
      
        $http({
            method: 'POST',
            url: 'api/ResGradeScales/ResGradeScalesGet',
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
                        $scope.GradeScaleList = response.obj
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATemplateByGScaleIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResGPATemplateGetByGScaleId',
            data: { GradeScaleId: $scope.ResBForAwarding.GradeScaleId },
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
                        $scope.GPATemplateList = response.obj;

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATemplateByGScaleIdAndEvalIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResGPATemplateGetByGScaleIdAndEvalId',
            data: {
                MstEvaluationId: $scope.ResBForAwarding.MstEvaluationId,
                GradeScaleId: $scope.ResBForAwarding.GradeScaleId,
            },
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
                        $scope.GPATemplateGScaleIdAndEvalIdList = response.obj;

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
            data: { Id: $scope.ResBForAwarding.GradeTemplateId },
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
                        $scope.MarksTempByIdList = {};
                        $scope.GPATemplateByIdList = response.obj[0];
                        $scope.getGPATempConfigByGradeTempIdList();
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
            data: { GradeTemplateId: $scope.ResBForAwarding.GradeTemplateId },
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
                        $scope.GradeTempConfigList = response.obj;
                    }
                    else {

                        if (response.obj != "")
                            $scope.GradeTempConfigList = response.obj;

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

    $scope.getResIncProgrammeInsGetByFacId = function () {

        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResIncProgrammeInsGetByFacId',
            data: { FacultyId: $scope.ResBForAwarding.FacultyId },
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
                        $scope.ProgInstList = response.obj;

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResIncProgInstancePartGetByProgInstId = function () {
      
            $http({
                method: 'POST',
                url: 'api/ResBasisForAwarding/ResIncProgInstancePartGetByProgInstId',
                data: {
                    ProgramInstanceId: $scope.ResBForAwarding.ProgramInstanceId,
                    MstProgrammeBranchMapId: $scope.ResBForAwarding.MstProgrammeBranchMapId
                },
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

                            $scope.ProgInstPartList = response.obj;
                            //   $scope.getResBForAwardingByProgInsIdList();

                            for (var i = 0; i < response.obj.length; i++) {

                                var j = i + 1;
                                if (j <= (response.obj.length - 1)) {
                                    if (response.obj[i].IncProgramInstancePartId == response.obj[j].IncProgramInstancePartId) {
                                        response.obj[j].PartName = "";
                                    }

                                }

                                else { break; }
                            }

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });       
    };

    $scope.getResBForAwardingList = function () {

        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResCalculationGet',
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

                        $scope.ResCalTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResBForAwardingByProgInsIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResBasisForAwarding/ResBasisForAwardingGetByProgInsId',
            data: {
                ProgramInstanceId: $scope.ResBForAwarding.ProgramInstanceId,
                MstProgrammeBranchMapId: $scope.ResBForAwarding.MstProgrammeBranchMapId
            },
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
                            alert("Record Already Filled");
                            for (var i = 0; i < response.obj.length; i++) {
                                if (response.obj[i].Applicable != "")
                                    $scope.ResBForAwarding.Applicable = response.obj[i].Applicable;
                                if (response.obj[i].MstEvaluationId != 0)
                                    $scope.ResBForAwarding.MstEvaluationId = response.obj[i].MstEvaluationId;
                                if ($scope.ResBForAwarding.MstEvaluationId == 3) {
                                    if (response.obj[i].ClassGradeTemplateId != 0) {
                                        $scope.ResBForAwarding.MarkTemplateId = response.obj[i].ClassGradeTemplateId;
                                        $scope.ResBForAwarding.ClassGradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                    }
                                }
                                else {
                                    if (response.obj[i].GradeScaleId != 0) {
                                        $scope.ResBForAwarding.GradeScaleId = response.obj[i].GradeScaleId;
                                        if (response.obj[i].ClassGradeTemplateId != 0) {
                                            $scope.ResBForAwarding.GradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                            $scope.ResBForAwarding.ClassGradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                        }
                                    }
                                }
                                if (response.obj[i].MinPassingPercentage != null)
                                    $scope.ResBForAwarding.MinPassingPercentage = response.obj[i].MinPassingPercentage;
                                if (response.obj[i].RoundingOption != "")
                                    $scope.ResBForAwarding.RoundingOption = response.obj[i].RoundingOption;
                                var j = i + 1;
                                if (j <= (response.obj.length - 1)) {
                                    if (response.obj[i].IncProgramInstancePartId == response.obj[j].IncProgramInstancePartId) {
                                        response.obj[j].PartName = "";

                                    }

                                }

                                else { break; }
                            }
                            $scope.ProgInstPartList = response.obj;
                            $scope.ShowResBForAwarding = true;
                            $scope.validationRBFAInFlag = true;
                            $scope.getResMstEvaluation();
                            if ($scope.ResBForAwarding.MstEvaluationId == 3)
                                $scope.getMarksTemplateList();
                            else {
                                $scope.getGradeScaleList();
                               // $scope.getGPATemplateByGScaleIdList();
                               $scope.getGPATemplateByGScaleIdAndEvalIdList();
                            }
                        }
                        else
                        {
                            $scope.ShowResBForAwarding = false;
                            $scope.validationRBFAInFlag = false;
                            $scope.ResBForAwarding.Applicable = null;
                            $scope.ResBForAwarding.MstEvaluationId = null;
                            $scope.ResBForAwarding.MarkTemplateId = null;
                            $scope.ResBForAwarding.GradeScaleId = null;
                            $scope.ResBForAwarding.GradeTemplateId = null;
                            $scope.ResBForAwarding.MinPassingPercentage = null;
                            $scope.ResBForAwarding.RoundingOption = null;
                            $scope.ProgInstPartList = {};
                            $scope.getResIncProgInstancePartGetByProgInstId();
                        }
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addResBForAwarding = function () {
     
        var count = 0;
        var validationFlag = false;
        if ($scope.ProgInstPartList == null || $scope.ProgInstPartList === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResBForAwarding')))
                    .clickOutsideToClose(true)
                    .title("Message")
                    .textContent("There is No Record")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
                if ($scope.ProgInstPartList[i].PartName == "Aggregate") {
                    // $scope.ProgInstPartList.splice(i, 1);
                }
                else {
                    $scope.ProgInstPartList[i].AggregatePercentage = $scope.ResBForAwarding.AggregatePercentage;
                }
            }

            for (var i = 0; i < ($scope.ProgInstPartList.length - 1); i++) {
                if (($scope.ProgInstPartList[i].ProgramInstancePartPercent == null || $scope.ProgInstPartList[i].ProgramInstancePartPercent == undefined) &&
                    ($scope.ProgInstPartList[i].ProgramInstancePartTermPercent == null || $scope.ProgInstPartList[i].ProgramInstancePartTermPercent == undefined)) {
                    count = count + 1;
                    if (count == ($scope.ProgInstPartList.length - 1)) {
                        alert("Please Fill Details");
                        validationFlag = false;
                    }
                }
                else {
                    validationFlag = true;
                }
            }
            if (validationFlag == true) {
                if ($scope.ShowResBForAwarding == false) {

                    var MstEvaluation = document.getElementById("Evaluationdropdown");
                    var MarkTemplate = document.getElementById("MarksTemplatedropdown");
                    var GradeScale = document.getElementById("GradeScaledropdown");
                    var GradeTemplate = document.getElementById("GradeTemplatedropdown");
                    if (MstEvaluation.value == null || MstEvaluation.value == "" || MstEvaluation.value === undefined) {
                        alert("Please Select Evaluation System ");
                    }
                    else {
                        if ($scope.ResBForAwarding.MstEvaluationId == 3) {
                            if (MarkTemplate.value == null || MarkTemplate.value == "" || MarkTemplate.value === undefined) {
                                alert("Please Select Mark Template Name ");
                            }
                            else {
                                $scope.ResBForAwarding.ClassGradeTemplateId = $scope.MarksTempByIdList.Id;
                                if ($scope.ResBForAwarding.Applicable != 'A' && $scope.ResBForAwarding.Applicable != 'P') { alert("Please Select Convocation 'Class\Grade' Awarded on the Basis of "); }
                                else {
                                    if ($scope.ResBForAwarding.MinPassingPercentage == null || $scope.ResBForAwarding.MinPassingPercentage == undefined) { alert("Please Enter Minimum Passing Percentage"); }
                                    else {
                                        if ($scope.ResBForAwarding.RoundingOption != 'R' && $scope.ResBForAwarding.RoundingOption != 'F' && $scope.ResBForAwarding.RoundingOption != 'C') { alert("Please Select Manage Fraction as "); }
                                        else { $scope.validationRBFAInFlag = true; }
                                    }
                                }

                                // alert("Mark  Option Selected");
                            }
                        }
                        else {
                            if (GradeScale.value == null || GradeScale.value == "" || GradeScale.value === undefined ||
                                GradeTemplate.value == null || GradeTemplate.value == "" || GradeTemplate.value === undefined) {
                                alert("Please Select Grade Scale and Grade Template Name");
                            }
                            else {
                                $scope.ResBForAwarding.ClassGradeTemplateId = $scope.GPATemplateByIdList.Id;
                                if ($scope.ResBForAwarding.Applicable != 'A' && $scope.ResBForAwarding.Applicable != 'P') { alert("Please Select Convocation 'Class\Grade' Awarded on the Basis of "); }
                                else { $scope.validationRBFAInFlag = true; }

                                //  alert("Grade  Option Selected");
                            }
                        }
                    }
                }
                if ($scope.validationRBFAInFlag == true) {
                    $scope.ResBForAwarding.InstPartTermListResBasis = $scope.ProgInstPartList;
                    $http({
                        method: 'POST',
                        url: 'api/ResBasisForAwarding/ResBForAwardingAdd',
                        data: $scope.ResBForAwarding,
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
                                    $scope.ResBForAwarding = {};
                                    $scope.ProgInstPartList = {};
                                    $scope.ShowTableFlag = false;
                                    $scope.ShowResBForAwarding = false;
                                }
                        })
                        .error(function (res) {
                            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                        });
                }
            }
        }
    };

    $scope.deleteResBForAwarding = function (ev) {
        if ($scope.ProgInstPartList == null || $scope.ProgInstPartList === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResBForAwarding')))
                    .clickOutsideToClose(true)
                    .title("Message")
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
                $scope.ResBForAwarding.InstPartTermListResBasis = $scope.ProgInstPartList;

                $http({
                    method: 'POST',
                    url: 'api/ResBasisForAwarding/ResBasisForAwardingDelete',
                    data: $scope.ResBForAwarding,
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
                                $scope.ResBForAwarding = {};
                                $scope.ProgInstPartList = {};
                                $scope.ShowTableFlag = false;
                                $scope.ShowResBForAwarding = false;
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

    $scope.MstEvalResBForAwarding = function () {
        $scope.ResBForAwarding.MarkTemplateId = null;
        $scope.ResBForAwarding.GradeScaleId = null;
        $scope.ResBForAwarding.GradeTemplateId = null;
       
        if ($scope.ResBForAwarding.MstEvaluationId == 3)
        {
            $scope.getMarksTemplateList();
        }
        else {
            $scope.getGradeScaleList();
        }
    };

    $scope.cancelResBForAwarding = function () {
        $scope.ResBForAwarding = {};
        $scope.modifyUserFlag = false;
    };

    $scope.getTotal = function () {
        var total = 0;
        for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
            var InstancePartdata = $scope.ProgInstPartList[i];
            total += (InstancePartdata.ProgramInstancePartPercent);
        }
        if (total <= 100) {
            $scope.ResBForAwarding.AggregatePercentage = total;
            document.getElementById("AggError").innerHTML = "";
            return total;
        }
        else
            document.getElementById("AggError").innerHTML = "Aggregate Should be Less than or equal to 100";

          //  alert("Aggregate Should be Less than or equal to 100");
    }

    $scope.getPartTermPercentTotal = function (k) {
        if ($scope.ProgInstPartList[k].ProgramInstancePartTermPercent <= 100)
        {
            document.getElementById(k + "_PTPercentError").innerHTML = "";
        }
        else
        {
            document.getElementById(k + "_PTPercentError").innerHTML = "Part Term (%) Should be <= 100";
        }
        };

    $scope.checkAllPartTermByPartId = function (PartId) {
        for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
            if ($scope.ProgInstPartList[i].IncProgramInstancePartId == PartId)
                $scope.ProgInstPartList[i].PartTermPercent = true;          
        }
    };
    $scope.AcademicYearGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/AcademicYearGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.AcademicList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.FacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/FacultyGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.FacultyList = {};

                    }
                }
                else {
                    $scope.FacultyList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.InstList = {};
        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ResBForAwarding,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.InstList = {};

                    }
                }
                else {
                    $scope.InstList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

});