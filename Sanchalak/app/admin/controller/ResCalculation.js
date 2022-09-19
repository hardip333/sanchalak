
app.controller('ResCalculationCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Result Calculation";

    $scope.cardTitle = "Result Calculation Operation";
    $scope.ResCal = {};

    //$scope.getResMstFaculty = function () {

    //    $http({
    //        method: 'POST',
    //        url: 'api/ResCalculation/ResMstFacultyGet',
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;
    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            else
    //                if (response.response_code != "200") {
    //                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //                }
    //                else {
    //                    $scope.FacultyList = response.obj;                   
    //                }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

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
            data: { Id: $scope.ResCal.MarkTemplateId },
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
            data: { MarkTemplateId: $scope.ResCal.MarkTemplateId },
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
            data: { GradeScaleId: $scope.ResCal.GradeScaleId },
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
                MstEvaluationId: $scope.ResCal.MstEvaluationId,
                GradeScaleId: $scope.ResCal.GradeScaleId,
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
            data: { Id: $scope.ResCal.GradeTemplateId },
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
            data: { GradeTemplateId: $scope.ResCal.GradeTemplateId },
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

    //$scope.getResIncProgrammeInsGetByFacId = function () {

    //    $http({
    //        method: 'POST',
    //        url: 'api/ResCalculation/ResIncProgrammeInsGetByFacId',
    //        data: { FacultyId: $scope.ResCal.FacultyId},
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;
    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            else
    //                if (response.response_code != "200") {
    //                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //                }
    //                else {
    //                    $scope.ProgInstList = response.obj;
                      
    //                }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

    $scope.getResIncProgInstancePartGetByProgInstId = function () {
        //var validationFlag = false;
        //var MstEvaluation = document.getElementById("Evaluationdropdown");
        //var MarkTemplate = document.getElementById("MarksTemplatedropdown");
        //var GradeScale = document.getElementById("GradeScaledropdown");
        //var GradeTemplate = document.getElementById("GradeTemplatedropdown");
        //if ($scope.ShowResCalc == false) {
        //    if (MstEvaluation.value == null || MstEvaluation.value == "" || MstEvaluation.value === undefined) {
        //        alert("Please Select Marks Evaluation");
        //    }
        //    else {
        //        if ($scope.ResCal.MstEvaluationId == 3) {
        //            if (MarkTemplate.value == null || MarkTemplate.value == "" || MarkTemplate.value === undefined) {
        //                alert("Please Select MarkTemplateId");
        //            }
        //            else {
        //                $scope.ResCal.ClassGradeTemplateId = $scope.MarksTempByIdList.Id;
        //                validationFlag = true;
        //               // alert("Mark  Option Selected");
        //            }
        //        }
        //        else {
        //            if (GradeScale.value == null || GradeScale.value == "" || GradeScale.value === undefined ||
        //                GradeTemplate.value == null || GradeTemplate.value == "" || GradeTemplate.value === undefined) {
        //                alert("Please Select Grade Scale and Grade Template ");
        //            }
        //            else {
        //                $scope.ResCal.ClassGradeTemplateId = $scope.GPATemplateByIdList.Id;
        //                validationFlag = true;
        //              //  alert("Grade  Option Selected");
        //            }
        //        }
        //    }
        //}
        //if (validationFlag == true) {
            $http({
                method: 'POST',
                url: 'api/ResCalculation/ResIncProgInstancePartGetByProgInstId',
                data: { ProgramInstanceId: $scope.ResCal.ProgramInstanceId },
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
                            // $scope.getResCalByProgInsIdList();
                            $scope.ShowTableFlag = true;
                            $scope.ShowResCalc = false;
                            $scope.validationFlagIn = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
       // }
    };

    $scope.getResCalList = function () {

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

    $scope.getResCalByProgInsIdList = function () {
        debugger;
        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResCalculationGetByProgInsId',
            data: { ProgramInstanceId: $scope.ResCal.ProgramInstanceId },
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
                            alert("Result Calculation is already Done");
                            for (var i = 0; i < response.obj.length; i++) {

                                $scope.ResCal.MstEvaluationId = response.obj[i].MstEvaluationId;
                                if ($scope.ResCal.MstEvaluationId == 3) {
                                    if (response.obj[i].ClassGradeTemplateId != 0)
                                        $scope.ResCal.MarkTemplateId = response.obj[i].ClassGradeTemplateId;
                                    $scope.ResCal.ClassGradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                }
                                else {
                                    if (response.obj[i].GradeScaleId != 0) {
                                        $scope.ResCal.GradeScaleId = response.obj[i].GradeScaleId;
                                        // if (response.obj[i].ClassGradeTemplateId != 0)
                                        $scope.ResCal.GradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                        $scope.ResCal.ClassGradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                    }
                                }

                                var j = i + 1;
                                if (j <= (response.obj.length - 1)) {
                                    if (response.obj[i].ProgramInstancePartId == response.obj[j].ProgramInstancePartId) {
                                        response.obj[j].PartName = "";
                                    }

                                }

                                else { break; }

                            }
                            $scope.ProgInstPartList = response.obj;
                            $scope.ShowResCalc = true;
                            $scope.validationFlagIn = true;
                            $scope.getResMstEvaluation();
                            if ($scope.ResCal.MstEvaluationId == 3)
                                $scope.getMarksTemplateList();
                            else {
                                $scope.getGradeScaleList();
                                //$scope.getGPATemplateByGScaleIdList();
                                $scope.getGPATemplateByGScaleIdAndEvalIdList();
                            }
                        }
                        else
                        {
                            $scope.ShowResCalc = false;
                            $scope.validationFlagIn = false;
                            $scope.ResCal.MstEvaluationId = null;
                            $scope.ResCal.MarkTemplateId = {};
                            $scope.ResCal.ClassGradeTemplateId = null;
                            $scope.ResCal.GradeScaleId = null;
                            $scope.ResCal.GradeTemplateId = {};
                            $scope.ProgInstPartList = {};
                            $scope.getResIncProgInstancePartGetByProgInstId();

                        }
                      
                    }
                debugger;
                    for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
                    if ($scope.ProgInstPartList[i].IsLaunched == true) {
                        $scope.deleteExemConfig = true;
                    }

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addResCal = function () {
        
        var count = 0;      
        var validationFlag = false;
        if ($scope.ProgInstPartList == null || $scope.ProgInstPartList === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResCalculation')))
                    .clickOutsideToClose(true)
                    .title("Message")
                    .textContent("There is No Record")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
                if ($scope.ProgInstPartList[i].IsResCalcApplicable == null || $scope.ProgInstPartList[i].IsResCalcApplicable === undefined ||
                    $scope.ProgInstPartList[i].AttachResultClass == null || $scope.ProgInstPartList[i].AttachResultClass === undefined ||
                    $scope.ProgInstPartList[i].ClassGradeTemplateId == null || $scope.ProgInstPartList[i].ClassGradeTemplateId === undefined

                ) {
                    validationFlag = false;
                }
                else {
                    if ($scope.ProgInstPartList[i].IsResCalcApplicable == false && $scope.ProgInstPartList[i].AttachResultClass == false) {
                        count = count + 1;
                        if (count == $scope.ProgInstPartList.length) {
                            alert("Please Select at least one checkbox");
                            $scope.getResCalByProgInsIdList();
                        }

                    }
                    else {
                        validationFlag = true;
                    }

                }
            }
        }
        if (validationFlag == true) {
            
            if ($scope.ShowResCalc == false) {
                var MstEvaluation = document.getElementById("Evaluationdropdown");
                var MarkTemplate = document.getElementById("MarksTemplatedropdown");
                var GradeScale = document.getElementById("GradeScaledropdown");
                var GradeTemplate = document.getElementById("GradeTemplatedropdown");
                if (MstEvaluation.value == null || MstEvaluation.value == "" || MstEvaluation.value === undefined) {
                    alert("Select Evaluation System");
                }
                else {
                    if ($scope.ResCal.MstEvaluationId == 3) {
                        if (MarkTemplate.value == null || MarkTemplate.value == "" || MarkTemplate.value === undefined) {
                            alert("Select Mark Template Name ");
                        }
                        else {
                            $scope.ResCal.ClassGradeTemplateId = $scope.MarksTempByIdList.Id;
                            $scope.validationFlagIn = true;
                            // alert("Mark  Option Selected");
                        }
                    }
                    else {
                        if (GradeScale.value == null || GradeScale.value == "" || GradeScale.value === undefined ||
                            GradeTemplate.value == null || GradeTemplate.value == "" || GradeTemplate.value === undefined) {
                            alert("Please Select Grade Scale and Grade Template Name ");
                        }
                        else {
                            $scope.ResCal.ClassGradeTemplateId = $scope.GPATemplateByIdList.Id;
                            $scope.validationFlagIn = true;
                            //  alert("Grade  Option Selected");
                        }
                    }
                }
            }
            if ($scope.validationFlagIn == true) {
                $scope.ResCal.InstPartTermList = $scope.ProgInstPartList;
                $http({
                    method: 'POST',
                    url: 'api/ResCalculation/ResCalculationAdd',
                    data: $scope.ResCal,
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
                                $scope.ResCal = {};
                                $scope.ShowTableFlag = false;
                                $scope.ShowResCalc = false;
                                $scope.ProgInstPartList = {};
                            }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        }
    };

    $scope.deleteResCal = function (ev) {
        if ($scope.ProgInstPartList == null || $scope.ProgInstPartList === undefined 
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResCalculation')))
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

                $scope.ResCal.InstPartTermList = $scope.ProgInstPartList;
                // $scope.GTempConfig = data;
                $http({
                    method: 'POST',
                    url: 'api/ResCalculation/ResCalculationDelete',
                    data: $scope.ResCal,
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
                                $scope.ResCal = {};
                                $scope.ShowTableFlag = false;
                                $scope.ShowResCalc = false;
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

    $scope.MstEvalResCal = function () {
        $scope.ResCal.MarkTemplateId = null;
        $scope.ResCal.GradeScaleId = null;
        $scope.ResCal.GradeTemplateId = null;

        if ($scope.ResCal.MstEvaluationId == 3) {
            $scope.getMarksTemplateList();
        }
        else {
            $scope.getGradeScaleList();
        }
    };

    $scope.cancelResCal = function () {
        $scope.ResCal = {};
        $scope.modifyUserFlag = false;
    };

    $scope.checkAllResCalc = function () {
            
        for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
            $scope.ProgInstPartList[i].IsResCalcApplicable = $scope.ResCal.SelAllCalc;
        }
       

    };

    $scope.checkAllResClass = function () {
        for (var i = 0; i < $scope.ProgInstPartList.length; i++) {
            $scope.ProgInstPartList[i].AttachResultClass = $scope.ResCal.SelAllClass;
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
        debugger;
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
                        $scope.GPATemplateAllList = {};
                        $scope.MarksTempList = {};
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
            data: $scope.ResCal,
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

    $scope.LaunchResConfiguration = function (data) {


        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResultConfigurationLaunch',
            data: $scope.ResCal,
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

                    /*$scope.getLaunchPartTermByFacIdAndAcadId();*/

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
});