
app.controller('ResConfigAPHCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Result Configuration APH";

    $scope.cardTitle = "Result Configuration APH Operation";
    $scope.APHConfig = {};

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
            data: { Id: $scope.APHConfig.MarkTemplateId },
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
            data: { MarkTemplateId: $scope.APHConfig.MarkTemplateId },
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
            data: { GradeScaleId: $scope.APHConfig.GradeScaleId },
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
                MstEvaluationId: $scope.APHConfig.MstEvaluationId,
                GradeScaleId: $scope.APHConfig.GradeScaleId,
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
            data: { Id: $scope.APHConfig.GradeTemplateId },
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
            data: { GradeTemplateId: $scope.APHConfig.GradeTemplateId },
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
            url: 'api/ResConfigAPH/ResIncProgrammeInsGetByFacId',
            data: { FacultyId: $scope.APHConfig.FacultyId, AcademicYearId: $scope.APHConfig.AcademicYearId },
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
                        
                        if ($scope.APHConfig.ProgramInstanceId == $localStorage.Details.ProgramInstanceId) {

                            if ($localStorage.Details.IncProgrammeInstancePartId) {
                                
                                $scope.APHConfig.IncProgrammeInstancePartId = $localStorage.Details.IncProgrammeInstancePartId;
                                $scope.getResIncProgInsPartByProgInstId();

                            }
                        }

                        else {
                            $localStorage.Details = {};
                        }


                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResIncProgInsPartByProgInstId = function () {

        $http({
            method: 'POST',
            url: 'api/ResConfigAPH/ResIncProgInsPartGetByProgInstId',
            data: { ProgrammeInstanceId: $scope.APHConfig.ProgramInstanceId },
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

                        if ($scope.APHConfig.IncProgrammeInstancePartId == $localStorage.Details.IncProgrammeInstancePartId) {
                            if ($localStorage.Details.IncProgrammeInstancePartTermId) {

                                $scope.APHConfig.IncProgrammeInstancePartTermId = $localStorage.Details.IncProgrammeInstancePartTermId;
                                $scope.getResIncProgInsPartTermByProgInstPartId();
                            }
                        }
                        else {
                            $localStorage.Details = {};
                        }
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResIncProgInsPartTermByProgInstPartId = function () {

        $http({
            method: 'POST',
            url: 'api/ResConfigAPH/ResIncProgInsPartTermGetByProgInstPartId',
            data: { IncProgrammeInstancePartId: $scope.APHConfig.IncProgrammeInstancePartId },
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
                        $scope.ProgInstPartTermList = response.obj;

                        if ($scope.APHConfig.IncProgrammeInstancePartTermId == $localStorage.Details.IncProgrammeInstancePartTermId) {
                            if ($localStorage.Details.IncProgrammeInstancePartTermId) {

                                $scope.APHConfig.IncProgrammeInstancePartTermId = $localStorage.Details.IncProgrammeInstancePartTermId;
                                $scope.getResIncProgrammeInfoGetByProgInsId();
                            }

                        }
                        else {
                            $localStorage.Details = {};
                        }
						
						

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResIncProgInstPartTermPaperMapByPartTermId = function () {
       
        $http({
            method: 'POST',
            url: 'api/ResConfigAPH/ResIncProgInstPartTermPaperMapGetByPTId',
            data: { IncProgrammeInstancePartTermId: $scope.APHConfig.IncProgrammeInstancePartTermId },
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
                        $scope.PaperByPartTermIdList = response.obj;

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResIncProgrammeInfoGetByProgInsId = function () {
      
        $http({
            method: 'POST',
            url: 'api/ResConfigAPH/ResIncProgrammeInfoGet',
            data: { IncProgrammeInstancePartTermId: $scope.APHConfig.IncProgrammeInstancePartTermId },
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
                        $scope.ProgInfoList = response.obj;
                        $scope.ShowTableFlag = true;
                        $scope.ShowAPHConfig = false;
                        $scope.validationFlagIn = false;

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //$scope.getResIncProgInstancePartGetByProgInstId = function () {
    //    //var validationFlag = false;
    //    //var MstEvaluation = document.getElementById("Evaluationdropdown");
    //    //var MarkTemplate = document.getElementById("MarksTemplatedropdown");
    //    //var GradeScale = document.getElementById("GradeScaledropdown");
    //    //var GradeTemplate = document.getElementById("GradeTemplatedropdown");
    //    //if ($scope.ShowResCalc == false) {
    //    //    if (MstEvaluation.value == null || MstEvaluation.value == "" || MstEvaluation.value === undefined) {
    //    //        alert("Please Select Marks Evaluation");
    //    //    }
    //    //    else {
    //    //        if ($scope.ResCal.MstEvaluationId == 3) {
    //    //            if (MarkTemplate.value == null || MarkTemplate.value == "" || MarkTemplate.value === undefined) {
    //    //                alert("Please Select MarkTemplateId");
    //    //            }
    //    //            else {
    //    //                $scope.ResCal.ClassGradeTemplateId = $scope.MarksTempByIdList.Id;
    //    //                validationFlag = true;
    //    //               // alert("Mark  Option Selected");
    //    //            }
    //    //        }
    //    //        else {
    //    //            if (GradeScale.value == null || GradeScale.value == "" || GradeScale.value === undefined ||
    //    //                GradeTemplate.value == null || GradeTemplate.value == "" || GradeTemplate.value === undefined) {
    //    //                alert("Please Select Grade Scale and Grade Template ");
    //    //            }
    //    //            else {
    //    //                $scope.ResCal.ClassGradeTemplateId = $scope.GPATemplateByIdList.Id;
    //    //                validationFlag = true;
    //    //              //  alert("Grade  Option Selected");
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //    //if (validationFlag == true) {
    //    $http({
    //        method: 'POST',
    //        url: 'api/ResCalculation/ResIncProgInstancePartGetByProgInstId',
    //        data: { ProgramInstanceId: $scope.APHConfig.ProgramInstanceId },
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

    //                    $scope.ProgInstPartList = response.obj;
    //                    // $scope.getResCalByProgInsIdList();
    //                    $scope.ShowTableFlag = true;
    //                    $scope.ShowResCalc = false;
    //                    $scope.validationFlagIn = false;
    //                }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //     }

    $scope.getResAPHList = function () {

            $http({
                method: 'POST',
                url: 'api/ResDefineAPH/ResDefineAPHGet',
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

                            //$scope.ResAPHTableParams = new NgTableParams({
                            //}, {
                            //    dataset: response.obj
                            //});
                            $scope.ResAPHList = response.obj;
                        }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        };

    $scope.getResAPHByIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResDefineAPH/ResDefineAPHGetById',
            data: { Id: $localStorage.APHId },
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
                        $scope.ResAPHByIdList = response.obj[0];
                        $scope.getResConfigAPHByAPHId();
                    }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResConfigAPHByAPHId = function () {

        $http({
            method: 'POST',
            url: 'api/ResConfigAPH/ResConfigAPHGetByAPHId',
            data: { APHId: $localStorage.APHId },
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
                            alert("Record Already Configured");
                            for (var i = 0; i < response.obj.length; i++) {
                                                           
                                $scope.APHConfig.FacultyId = response.obj[0].FacultyId;
                                $scope.APHConfig.ProgramInstanceId = response.obj[0].ProgrammeInstanceId;
                                $scope.APHConfig.IncProgrammeInstancePartId = response.obj[0].IncProgrammeInstancePartId;
                                $scope.APHConfig.IncProgrammeInstancePartTermId = response.obj[0].IncProgrammeInstancePartTermId;
                                $scope.APHConfig.APHMinMarks = response.obj[0].APHMinMarks;
                             
                                $scope.APHConfig.MstEvaluationId = response.obj[i].MstEvaluationId;
                                if ($scope.APHConfig.MstEvaluationId == 3) {
                                    if (response.obj[i].ClassGradeTemplateId != 0)
                                        $scope.APHConfig.MarkTemplateId = response.obj[i].ClassGradeTemplateId;
                                    $scope.APHConfig.ClassGradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                }
                                else {
                                    if (response.obj[i].GradeScaleId != 0) {
                                        $scope.APHConfig.GradeScaleId = response.obj[i].GradeScaleId;
                                        // if (response.obj[i].ClassGradeTemplateId != 0)
                                        $scope.APHConfig.GradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                        $scope.APHConfig.ClassGradeTemplateId = response.obj[i].ClassGradeTemplateId;
                                    }
                                }

                            }
                            $scope.ProgInfoList = response.obj;
                            $scope.ShowAPHConfig = true;
                            $scope.validationFlagIn = true;
                           
                           // $scope.getResMstFaculty(); 
                            $scope.getResIncProgrammeInsGetByFacId();
                            $scope.getResIncProgInsPartByProgInstId();
                            $scope.getResIncProgInsPartTermByProgInstPartId();
                            $scope.getResMstEvaluation();
                            if ($scope.APHConfig.MstEvaluationId == 3)
                                $scope.getMarksTemplateList();
                            else {
                                $scope.getGradeScaleList();
                                //$scope.getGPATemplateByGScaleIdList();
                                $scope.getGPATemplateByGScaleIdAndEvalIdList();
                            }
                        }
                        else {
                           // $scope.ShowResCalc = false;
                           // $scope.validationFlagIn = false;
                           // $scope.APHConfig.MstEvaluationId = null;
                           // $scope.APHConfig.MarkTemplateId = null;
                           // $scope.APHConfig.ClassGradeTemplateId = null;
                           // $scope.APHConfig.GradeScaleId = null;
                           // $scope.APHConfig.GradeTemplateId = null;
                           // $scope.ProgInfoList = {};
                           //// $scope.getResMstFaculty();
                           //// $scope.getResIncProgrammeInfoGetByProgInsId();
                        }

                        //$scope.ResAPHConfgByAPHIdList = response.obj;
                        
                    }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getAPHConfigList = function () {

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

                        $scope.APHConfigTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getAPHConfigByProgInsIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResCalculationGetByProgInsId',
            data: { ProgramInstanceId: $scope.APHConfig.ProgramInstanceId },
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
                            alert("Record Already Configured");
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
                        else {
                            $scope.ShowResCalc = false;
                            $scope.validationFlagIn = false;
                            $scope.ResCal.MstEvaluationId = null;
                            $scope.ResCal.MarkTemplateId = null;
                            $scope.ResCal.ClassGradeTemplateId = null;
                            $scope.ResCal.GradeScaleId = null;
                            $scope.ResCal.GradeTemplateId = null;
                            $scope.ProgInstPartList = {};
                            $scope.getResIncProgInstancePartGetByProgInstId();
                        }

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.addAPHConfig = function () {

        var Faculty = document.getElementById("Facultydropdown");
        var ProgInst = document.getElementById("ProgInstdropdown");
        var ProgInstPart = document.getElementById("ProgInstPartdropdown");
        var ProgInstPertTerm = document.getElementById("ProgInstPertTermdropdown");
      //  var count = 0;
        var countPaper = 0;
        var validationFlag = false;

        if (Faculty.value == null || Faculty.value == "" || Faculty.value === undefined) {
            Faculty.focus();
            alert("Select Faculty Name");
        }
        else if (ProgInst.value == null || ProgInst.value == "" || ProgInst.value === undefined) {
            ProgInst.focus();
            alert("Select Programme Name");
        }
        else if (ProgInstPart.value == null || ProgInstPart.value == "" || ProgInstPart.value === undefined) {
            ProgInstPart.focus();
            alert("Select Programme Part Name");
        }
        else if (ProgInstPertTerm.value == null || ProgInstPertTerm.value == "" || ProgInstPertTerm.value === undefined) {
            ProgInstPertTerm.focus();
            alert("Select Programme Part Term Name");
        }
        else {
            for (var i = 0; i < $scope.ProgInfoList.length; i++) {
                //if ($scope.ProgInfoList[i].CheckPaper == false) {
                //    count = count + 1;
                //    if (count == $scope.ProgInfoList.length) {
                //        alert("Please Select at least two Paper checkbox");
                //        // $scope.getResCalByProgInsIdList();
                //    }

                //}
                //else {
                    if ($scope.ProgInfoList[i].CheckPaper == true) {
                        countPaper = countPaper + 1;
                    }

              //  }

            }
            if (countPaper >= 2) {
                if ($scope.APHConfig.APHMinMarks == null || $scope.APHConfig.APHMinMarks === undefined)
                {
                    document.getElementById("APHMinMarks1").focus();
                    alert("Enter APH Minimum Marks");
                }
                else { validationFlag = true;}              
            }
            else {
                alert("Please Select at least Two Papers");
            }
        }
                         
        if (validationFlag == true) {
            if ($scope.ShowAPHConfig == false) {
                var MstEvaluation = document.getElementById("Evaluationdropdown");
                var MarkTemplate = document.getElementById("MarksTemplatedropdown");
                var GradeScale = document.getElementById("GradeScaledropdown");
                var GradeTemplate = document.getElementById("GradeTemplatedropdown");
                if (MstEvaluation.value == null || MstEvaluation.value == "" || MstEvaluation.value === undefined) {
                    MstEvaluation.focus();
                    alert("Select Evaluation System");
                }
                else {
                    if ($scope.APHConfig.MstEvaluationId == 3) {
                        if (MarkTemplate.value == null || MarkTemplate.value == "" || MarkTemplate.value === undefined) {
                            MarkTemplate.focus();
                            alert("Select Mark Template Name ");
                        }
                        else {
                            $scope.APHConfig.ClassGradeTemplateId = $scope.MarksTempByIdList.Id;
                            $scope.validationFlagIn = true;
                        }
                    }
                    else {
                        if (GradeScale.value == null || GradeScale.value == "" || GradeScale.value === undefined ||
                            GradeTemplate.value == null || GradeTemplate.value == "" || GradeTemplate.value === undefined) {
                            GradeScale.focus();
                            GradeTemplate.focus();
                            alert("Please Select Grade Scale and Grade Template Name ");
                        }
                        else {
                            $scope.APHConfig.ClassGradeTemplateId = $scope.GPATemplateByIdList.Id;
                            $scope.validationFlagIn = true;
                        }
                    }
                }
            }
            if ($scope.validationFlagIn == true) {
                $scope.APHConfig.APHId = $localStorage.APHId;
                $scope.APHConfig.APHConfgList = $scope.ProgInfoList;
                $http({
                    method: 'POST',
                    url: 'api/ResConfigAPH/ResConfigAPHAdd',
                    data: $scope.APHConfig,
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
                                
                                $localStorage.Details = {};
                                $localStorage.Details.AcademicYearId = $scope.APHConfig.AcademicYearId;
                                $localStorage.Details.FacultyId = $scope.APHConfig.FacultyId;
                                $localStorage.Details.ProgramInstanceId = $scope.APHConfig.ProgramInstanceId;
                                $localStorage.Details.IncProgrammeInstancePartId = $scope.APHConfig.IncProgrammeInstancePartId;
                                $localStorage.Details.IncProgrammeInstancePartTermId = $scope.APHConfig.IncProgrammeInstancePartTermId;
                                alert(response.obj);
                                /*$scope.APHConfig = {};*/
                                $scope.ShowTableFlag = false;
                                $scope.ShowAPHConfig = false;
                                $scope.ProgInfoList = {};
                                $state.go('AdditionalPassingHeadEdit');
                                //$scope.getResConfigAPHByAPHId();
                            }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        }
    };

    $scope.deleteAPHConfig = function (ev) {
        if ($scope.ProgInfoList == null || $scope.ProgInfoList === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResAPHConfig')))
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
                $scope.APHConfig.APHId = $localStorage.APHId;
                $scope.APHConfig.APHConfgList = $scope.ProgInfoList;

                $http({
                    method: 'POST',
                    url: 'api/ResConfigAPH/ResConfigAPHDelete',
                    data: $scope.APHConfig,
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
                                $scope.APHConfig = {};
                                $scope.ShowTableFlag = false;
                                $scope.ShowAPHConfig = false;
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
        $scope.APHConfig.MarkTemplateId = null;
        $scope.APHConfig.GradeScaleId = null;
        $scope.APHConfig.GradeTemplateId = null;

        if ($scope.APHConfig.MstEvaluationId == 3) {
            $scope.getMarksTemplateList();
        }
        else {
            $scope.getGradeScaleList();
        }
    };

    $scope.cancelAPHConfig = function () {
        $scope.APHConfig = {};
        $scope.modifyUserFlag = false;
    };

    $scope.checkAllAPHConfig = function () {
        for (var i = 0; i < $scope.ProgInfoList.length; i++) {
            $scope.ProgInfoList[i].CheckPaper = $scope.APHConfig.SelAllPaper;
        }

    };

    $scope.cancelAPHConfig = function () {
        $scope.APHConfig = {};
        $scope.modifyUserFlag = false;
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
                    
                    if ($localStorage.Details.AcademicYearId != null || $localStorage.Details.AcademicYearId != undefined) {
                        
                        $scope.APHConfig.AcademicYearId = $localStorage.Details.AcademicYearId;
                        $scope.APHConfig.FacultyId = $localStorage.Details.FacultyId;
                        $scope.FacultyGet();
                    }
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
                    if ($scope.APHConfig.FacultyId == $localStorage.Details.FacultyId) {
                        if ($localStorage.Details.ProgramInstanceId) {
                            
                            $scope.APHConfig.ProgramInstanceId = $localStorage.Details.ProgramInstanceId;

                            $scope.getResIncProgrammeInsGetByFacId();

                        }
                    }
                    else {

                        $localStorage.Details = {};
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

   
   
});