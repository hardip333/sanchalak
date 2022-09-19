app.controller('ResCourseEvalSystemCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Result Course Evaluation System";
    $scope.ADFP = {};
    $scope.ADFP.EvaluationList = {};
    $scope.EvaluationList1 = {};
    


    $scope.showdrop = function () {
        var count = 0;
        for (var i = 0; i < $scope.PaperListforResEvalSystem.length; i++) {
            if ($scope.PaperListforResEvalSystem[i].IsCheckSelect == null || $scope.PaperListforResEvalSystem[i].IsCheckSelect === undefined
                || $scope.PaperListforResEvalSystem[i].IsCheckSelect == false
            ) {
                continue;

            }
            else {
                count = count + 1;
            }
        }
        if (count > 0) {
            $scope.showdropdown = true;
         
        }
        else {
            $scope.showdropdown = false;
        }
        


        
    }

    //Check atleast one checkbox
    $scope.AtLeastOneCheckforResEval = function () {
        
        //$scope.ADFP.MstEvaluationId = $scope.ADFP.MstEvaluation.Id;
        
        var count=0;
        for (var i = 0; i < $scope.PaperListforResEvalSystem.length; i++) {
            if ($scope.PaperListforResEvalSystem[i].IsCheckSelect == null || $scope.PaperListforResEvalSystem[i].IsCheckSelect === undefined
                || $scope.PaperListforResEvalSystem[i].IsCheckSelect == false
            ) {
                continue;

            }
            else {
                count = count + 1;
            }
        }
        if (count>0) {
            $scope.CheckforResEval();
            
        }
        else {
            alert("Please Select at least one checkbox");
        }

    }
    //TLMAM Parent check
    $scope.CheckforResEval = function () {
        
        let flag1 = true;
        for (var i = 0; i < $scope.PaperListforResEvalSystem.length; i++) {


            if ($scope.PaperListforResEvalSystem[i].PaperName == null && $scope.PaperListforResEvalSystem[i].IsCheckSelect == true) {

                for (var j = 0; j < $scope.PaperListforResEvalSystem.length; j++) {

                    if ($scope.PaperListforResEvalSystem[i].MstPaperId == $scope.PaperListforResEvalSystem[j].MstPaperId && $scope.PaperListforResEvalSystem[j].PaperName != null) {

                        if ($scope.PaperListforResEvalSystem[j].IsCheckSelect != true) {
                            
                            
                            flag1 = false;
                            
                            break;
                        }
                    }

                }

            }
            


        }
        if (flag1 == true) {
            $scope.DropdownCheckforResEval();
        }
        else {
            alert("Please Select Paper");
        }

    }
    //Check Eva dropdown
    $scope.DropdownCheckforResEval = function () {
        var Flag3 = false;
        var MstEvaluation = document.getElementById("Evaluationdropdown");
        var MarkTemplate = document.getElementById("MarksTemplatedropdown");
        var GradeScale = document.getElementById("GradeScaledropdown");
        var GradeTemplate = document.getElementById("GradeTemplatedropdown");
        if (MstEvaluation.value == null || MstEvaluation.value == "" || MstEvaluation.value === undefined) {
            alert("Select Evaluation System");
        }
        else {
            if ($scope.ADFP.MstEvaluationId == 3) {
                if (MarkTemplate.value == null || MarkTemplate.value == "" || MarkTemplate.value === undefined) {
                    alert("Select Mark Template Name ");
                }
                else {
                    $scope.ADFP.ClassGradeTemplateId = $scope.MarksTempByIdList.Id;
                    Flag3 = false;

                }
            }
            else {
                if (GradeScale.value == null || GradeScale.value == "" || GradeScale.value === undefined ||
                    GradeTemplate.value == null || GradeTemplate.value == "" || GradeTemplate.value === undefined) {
                    alert("Please Select Grade Scale and Grade Template Name ");
                }
                else {
                    $scope.ADFP.ClassGradeTemplateId = $scope.GPATemplateByIdList.Id;
                    Flag3 = false;
                }
            }
            if (Flag3 = true) {
                $scope.addResEval();
            }
        }




    }
    //Add after check

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }



    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.addResEval = function () {
        debugger;
        $scope.ADFP.PaperList1 = $scope.PaperListforResEvalSystem;
        //$scope.ADFP.IncProgrammeInstancePartTermId = $scope.PaperListforResEvalSystem[0].IncProgrammeInstancePartTermId;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/ResEvaluationAdd',
            data: $scope.ADFP,
            eaders: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        alert("Weightage should be below 100");
                        $scope.offSpinner();
                    }
                }
                else {
                    $scope.offSpinner();
                    alert(response.obj);
                    $scope.ADFP = {};

                    $scope.PaperListforResEvalSystem = {};

                }
            })
            .error(function (res) {
                alert(res.obj);
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.deleteResEval = function (ev) {
        if ($scope.PaperListforResEvalSystem == null || $scope.PaperListforResEvalSystem === undefined
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
               
                $scope.ADFP.PaperList1 = $scope.PaperListforResEvalSystem;
                // $scope.GTempConfig = data;
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/ResCourseEvalSystem/ResEvaluationDelete',
                    data: $scope.ADFP,
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
                                $scope.offSpinner();
                                alert(response.obj);
                                $scope.ADFP = {};
                                
                                
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



    //------------------------------------------------------------------------------------------------------------
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
            data: $scope.ADFP,
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
    $scope.getProgrammePartListByProgInstId = function () {
        //$scope.FEECONFIG.ProgrammeInstanceId = $scope.FEECONFIG.ProgrammeInstanceId;


        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/ProgrammePartGetByProgInstId',
            data: $scope.ADFP,
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
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getProgPartTermListByProgInstPartId = function () {
        $scope.ProgPartTermList = {};
        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/ProgrammePartTermGetByProgInstId',
            data: $scope.ADFP,
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
                        $scope.ProgPartTermList = {};

                    }
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getPaperListforResEvalSystem = function () {

        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/FinalViewforEvaluationSystem',
            data: $scope.ADFP,
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

                    $scope.PaperListforResEvalSystem = response.obj;
                   
                    for (var i = 0; i < response.obj.length; i++) {

                        if (response.obj[i].IsCheckSelectSts == true && response.obj[i].IsCheckSelect == true) {
                            $scope.ShowResEval = true;
                            $scope.ShowResWeightage = true;
                        }
                        else {
                            $scope.ShowResEval = false;
                            //$scope.ShowResWeightage = false;
                            
                        }
                    }
                    debugger;
                    for (var i = 0; i < $scope.PaperListforResEvalSystem.length; i++) {
                        if ($scope.PaperListforResEvalSystem[i].IsLaunched == true) {
                            $scope.deleteExemConfig = true;
                        }

                    }
                    
                    



                    $scope.getResMstEvaluation();
                    if ($scope.ADFP.MstEvaluationId == 3)
                        $scope.getMarksTemplateList();
                    else {

                        $scope.getGPATemplateByGScaleIdAndEvalIdList();
                    }
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.getResMstEvaluation = function () {
        //count = count + 1;


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


                        //else {
                        $scope.EvaluationList1 = response.obj;
                        /*}*/

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
    
    
    $scope.checkAll = function () {

        for (var i = 0; i < $scope.PaperListforResEvalSystem.length; i++) {
            $scope.PaperListforResEvalSystem[i].IsCheckSelect = $scope.ADFP.SelAllEval;
        }

    };
    $scope.MstEvalResCal = function () {
        $scope.ADFP.ClassGradeTemplateId = null;
        $scope.ADFP.GradeScaleId = null;
        $scope.ADFP.GradeTemplateId = null;


        //for (key in Object.keys($scope.EvaluationList1)) {
        //    if ($scope.ADFP.MstEvaluationId == $scope.EvaluationList1[key].Id) {
        //        for (var i in $scope.PaperListforResEvalSystem) {
        //            $scope.PaperListforResEvalSystem[i].EvaluationName = $scope.EvaluationList1[key].EvaluationName;
        //        }
        //    }
        //}
        //console.log($scope.MarksTempConfigList);
        //console.log($scope.MarksTempList);
        //console.log($scope.GPATemplateByIdList);
        //console.log($scope.MarksTempList);
        //for (key in Object.keys($scope.getGPATemplateByGScaleIdAndEvalIdList1)) {
        //    if ($scope.ADFP.MstEvaluationId && $scope.ADFP.GradeScaleId  == $scope.getGPATemplateByGScaleIdAndEvalIdList1[key].Id) {
        //        for (var i in $scope.PaperListforResEvalSystem) {
        //            $scope.PaperListforResEvalSystem[i].TemplateName = $scope.getGPATemplateByGScaleIdAndEvalIdList1[key].TemplateName;
        //        }
        //    }
        //}


        if ($scope.ADFP.MstEvaluationId == 3) {
            //$scope.ADFP.EvaluationList = {};


            $scope.getMarksTemplateList();

        }
        else {
            $scope.getGradeScaleList();
        }
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
            data: { Id: $scope.ADFP.MarkTemplateId },
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
            data: { MarkTemplateId: $scope.ADFP.MarkTemplateId },
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
    $scope.getGPATemplateByGScaleIdAndEvalIdList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResGPATemplateGetByGScaleIdAndEvalId',
            data: {
                MstEvaluationId: $scope.ADFP.MstEvaluationId,
                GradeScaleId: $scope.ADFP.GradeScaleId,
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
            data: { Id: $scope.ADFP.GradeTemplateId },
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
            data: { GradeTemplateId: $scope.ADFP.GradeTemplateId },
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

    $scope.LaunchResConfiguration = function (data) {


        $http({
            method: 'POST',
            url: 'api/ResCourseEvalSystem/ResultConfigurationLaunch',
            data: $scope.ADFP,
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