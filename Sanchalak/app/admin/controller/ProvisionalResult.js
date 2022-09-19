app.controller('ProvisionalResultController', function ($scope, $http, $rootScope, $window, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    //debugger;
    $scope.Student = {};
    $scope.isPart = true;
   

    $scope.printDiv = function (areaID) {
        //debugger;
        var contents = document.getElementById(areaID).innerHTML;
        var othercontents = '<html>' + '<head><style type="text/css" media="print">' +
            '.watermark {opacity: 0.2; font-size: 100px; color: grey; background: #FFFFFF; position: absolute; cursor: default ; user - select: none; -webkit - user - select: none; -moz - user - select: none; -ms - user - select: none; top: 370px; left: 80px; right: 5px; bottom: 5px;}' +
            '@page { size: A4 Portriat; margin: 1.5%; }  </style > ' +
            '<link href="/../Vidhyarthi/bower_components/bootstrap/css/bootstrap.min.css" media="print" rel="stylesheet" type="text/css">' +
            /* '<link href = "/../../Styles/custom.css" </head >' +*/
            '<body style = "border: 2px solid black" >' +
            contents +
            '<script type = "text/javascript" > function printPage() { window.focus(); window.print(); return; }</script> ' +
            '</body></html>'

        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);

        frame1.contentWindow.contents = othercontents;
        frame1.src = 'javascript:window["contents"]';
        frame1.focus();
        setTimeout(function () {
            frame1.contentWindow.printPage();
        }, 200);
    }

    //---------------getting previous year results-----------------
    //$scope.GetPreviousYearResults = function () {
    //    $http({
    //        method: 'POST',
    //        url: 'api/ProvisionalResult/GenerateProvisionalResult?ProgId=' + param[0][0] + '&reqId=' + param[0][1] + '&CertiId=' + param[0][2],

    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootscope.showLoading = false;
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                $scope.PreviousYearResults = response.obj;
    //            }
    //        })

    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        })
    //}
    //-------over-----------

    //$scope.GetResultParameters = function () {
    //    //debugger;
    //    $scope.ProgId = {};
    //    $scope.ProgId = $localStorage.IncStudAdmGetProgData[0].ProgrammeId;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ProvisionalResult/GetResultParameters?ProgId=' + $scope.ProgId ,
    //        //data: $scope.ProgId,
    //        headers: { "Content-Type": 'application/json' }
    //    })

    //        .success(function (response) {
    //            $rootScope.showLoading = false;
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                //debugger;
    //                $scope.ResultParameters = response.obj;
    //                $localStorage.ResultParameters = response.obj;

    //                //var today = new Date();
    //                //$scope.date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
    //                //if ($scope.Student.IssueDate) {
    //                //    let str1 = $scope.Student.IssueDate;
    //                //    $scope.myArr1 = str1.split(" ");
    //                //}
    //               // generateQRCode($scope.firstRecord.PRN, param[0][0], param[0][2]);

    //            }
    //        })

    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        })

    //}

    $scope.StudentDashboard = {};

    $scope.GenerateProvisionalResult = function () {
        //debugger;
        $scope.StudentDashboard = $localStorage.IncStudAdm;
        $http({
            method: 'POST',
            url: 'api/ProvisionalResult/GenerateProvisionalResult',
            data: $scope.StudentDashboard,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                
                if (response.response_code == "404") {

                    alert('RESULT NOT DECLARED OR ADMISSION FEE IS NOT FULLY PAID!!');
                }
                else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //debugger;
                    $scope.ResultObtained = response.obj;

                    /*var today = new Date();
                    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

                    if ($scope.ResultObtained.ObjStudConsolidatedResult.ResultDeclaredDate == "" || $scope.ResultObtained.ObjStudConsolidatedResult.ResultDeclaredDate == null) { //$scope.ResultObtained.ObjStudConsolidatedResult.ResultDeclaredDate != date) {
                        alert('RESULT NOT DECLARED!!');
                        $state.go('studentDashboard');
                    }*/

                    //full result condition check start
                    //else {
                    //checking RR
                    if ($scope.ResultObtained.ObjStudConsolidatedResult.ResultStatus == "RR" || $scope.ResultObtained.ObjStudConsolidatedResult.ResultStatus == "UFM" || $scope.ResultObtained.ObjStudConsolidatedResult.ResultStatus == "EHB") {
                        //debugger;
                        alert('Result held back!!');
                        $state.go('ApplicantPreExaminationDetails');
                    }

                    if ($scope.ResultObtained.ObjStudConsolidatedResult.PartStatus == "TDL") {
                        //debugger;
                        alert('Result to be Declared Later.');
                        $state.go('ApplicantPreExaminationDetails');
                    }

                    if ($scope.ResultObtained.ObjStudConsolidatedResult.HeldBackStatus == "EHB") {
                        //debugger;
                        alert('Result held back!!');
                        $state.go('ApplicantPreExaminationDetails');
                    }

					$scope.printdatetime = new Date();
					$scope.printdate = $scope.printdatetime.getDate() + '-' + ($scope.printdatetime.getMonth()+1) + '-' + $scope.printdatetime.getFullYear() + ' ' + '|' + ' ' + $scope.printdatetime.getHours() + ':' + $scope.printdatetime.getMinutes() + ':' + $scope.printdatetime.getSeconds();

                    if ($scope.ResultObtained.ObjLstPaperResultModel[0].EvaluationId == 3) {
                            //debugger;
                            if ($scope.ResultObtained.ObjStudConsolidatedResult.IsPart == true) {
                                $scope.isPart = false;
                            }
                            //checking ordincance
                            if ($scope.ResultObtained.ObjStudConsolidatedResult.Ordinance == null || $scope.ResultObtained.ObjStudConsolidatedResult.Ordinance == "") {
                                $scope.ordinanceapplied = 'NOT APPLIED';
                            }
                            else {
                                $scope.ordinanceapplied = '*APPLIED';
                            }

                            if ($scope.ResultObtained.ObjStudConsolidatedResult.ResultStatus == "UFM") {
                                $scope.ResultObtained.ObjStudConsolidatedResult.MarksObtained = "UFM";
                                $scope.ResultObtained.ObjStudConsolidatedResult.MarkstoWords = "Un-Fair Means";
                            }

                            for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {

                                //debugger;
                                //checking for overall absence in paper
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IsAbsent == true) {
                                    debugger;
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks = 'ABS';
                                    if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == null) {
                                        $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = '--';
                                    }
                                    else {
                                        $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = 'ABS';
                                    }
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks = 'ABS';
                                }

                                else {
                                    for (var k = 0; k < $scope.ResultObtained.ObjStudConsolidatedResult.ObjLstResMarkGradeEntryCount; ++k) {
                                        if ($scope.ResultObtained.ObjLstPaperResultModel[i].PaperId == $scope.ResultObtained.ObjLstResMarkGradeEntry[k].PaperId) {

                                            debugger;
                                            //sir 
                                            if ($scope.ResultObtained.ObjLstResMarkGradeEntry[k].AssessmentType == 'IA') {
                                                if ($scope.ResultObtained.ObjLstResMarkGradeEntry[k].IsAbsent == true) {
                                                    //if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == null) {
                                                    //    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "--";
                                                    //}

                                                    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = 'ABS';

													continue;
                                                }
                                            }
                                            else {
                                                if ($scope.ResultObtained.ObjLstResMarkGradeEntry[k].IsAbsent == true) {
                                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks = 'ABS';
                                                    // break;
                                                }
                                            }

                                            //break;
                                        }
                                        else {
                                            continue;
                                        }
                                    }
                                }

                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IsUFM == true) {
                                    //debugger;
                                    for (var k = 0; k < $scope.ResultObtained.ObjStudConsolidatedResult.ObjLstResMarkGradeEntryCount; ++k) {
                                        if ($scope.ResultObtained.ObjLstPaperResultModel[i].PaperId == $scope.ResultObtained.ObjLstResMarkGradeEntry[k].PaperId) {
                                            //checking for individual UFM----also do for direct indirect if isufm true individiually but grade != f and egp != 0

                                            if ($scope.ResultObtained.ObjLstResMarkGradeEntry[k].AssessmentType == 'IA') {
                                                if ($scope.ResultObtained.ObjLstResMarkGradeEntry[k].IsUFM == true) {
                                                    //if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == null) {
                                                    //    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "--";
                                                    //}
                                                    //else {
                                                    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = 'UFM';
                                                    //}
                                                }
                                            }
                                            else {
                                                if ($scope.ResultObtained.ObjLstResMarkGradeEntry[k].IsUFM == true) {
                                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks = 'UFM';
                                                    $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks = 'UFM';
                                                }
                                            }
                                            break;
                                        }

                                        else {
                                            continue;
                                        }
                                    }
                                }

                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMaxMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMaxMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMaxMarks == 0) {
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMaxMarks = "--";
                                }
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMinMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMinMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMinMarks == 0) {
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMinMarks = "--";
                                }

                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].UAMaxMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMaxMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMaxMarks == 0) {
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMaxMarks = "--";
                                }

                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].UAMinMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMinMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMinMarks == 0) {
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMinMarks = "--";
                                }
								
								
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == null) {

                                    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "--";
                                }

                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == null) {

                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks = "--";
                                }

                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == "--") {
                                    //debugger;
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = 0;
                                }


                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == "--") {
									//debugger;
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks = 0;
                                }
								
								
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks == null) {
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks = "--";
                                }

                                //checking for ordinance

                            }

                            //individual loops to check null or blank of a mark value while taking total
                            var x = 0, y = 0, z = 0, maxmarkstotal = 0;
                            for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {
                                debugger;
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == 'ABS' || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == 'UFM') {
                                    continue;
                                }
                                else {
                                    x += $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks;
                                }
                            }
                            for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {
                                
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == 'ABS' || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == 'UFM' || $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks == 'ABS' || $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks == '--') {
                                    continue;
                                }
                                else {
                                    y += $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks;
                                }
                            }
                            for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {
                                
                                if ($scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks == null || $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks == 'ABS' || $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks == 'UFM') {
                                    continue;
                                }
                                else {
                                    z += $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks;
                                    if ($scope.ResultObtained.ObjLstPaperResultModel[i].EarningMarks != null || $scope.ResultObtained.ObjLstPaperResultModel[i].AdhocMarks != null) {
                                        $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks = $scope.ResultObtained.ObjLstPaperResultModel[i].FinalMarks + '*';
                                    }
                                }
                            }
							
							for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {
								debugger;
								if ($scope.ResultObtained.ObjLstPaperResultModel[i].MaxMarks == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].MaxMarks == null) {
									continue;
								}
								else {
									maxmarkstotal += $scope.ResultObtained.ObjLstPaperResultModel[i].MaxMarks;
								}
							}
							
							for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {
								if ($scope.ResultObtained.ObjLstPaperResultModel[i].UAMaxMarks == "--" && $scope.ResultObtained.ObjLstPaperResultModel[i].UAMinMarks == "--") {
                                    //debugger;
                                    $scope.ResultObtained.ObjLstPaperResultModel[i].UAMarks = "--";
								}
				
								if ($scope.ResultObtained.ObjLstPaperResultModel[i].IAMaxMarks == "--" && $scope.ResultObtained.ObjLstPaperResultModel[i].IAMinMarks == "--") {
                            		 //debugger;
                           		 $scope.ResultObtained.ObjLstPaperResultModel[i].IAMarks = "--";
								}
							}



                            $scope.TotalUAMarks = x;
                            if (x == null || x == "") {
                                $scope.TotalUAMarks = "--";
                            }
                            $scope.TotalIAMarks = y;
                            if (y == null || y == "") {
                                $scope.TotalIAMarks = "--";
                            }
                            $scope.TotalFinalMarks = z;
                            if (z == null || z == "") {
                                $scope.TotalFinalMarks = "--";
                            }
							$scope.TotalMaxMarks = maxmarkstotal;
							if (maxmarkstotal == null || maxmarkstotal == "") {
								$scope.TotalMaxMarks = "--";
							}
                    }
                    else {
                        debugger;
                        //to be removed added for testing only
                        if ($scope.ResultObtained.ObjStudConsolidatedResult.IsPart == true) {
                                $scope.isPart = false;
								if($scope.ResultObtained.ObjStudConsolidatedResult.PartStatus == "ATKT"){
									$scope.ResultObtained.ObjStudConsolidatedResult.PartStatus = "Incomplete";
								}
                        }
                        //till here
						
						

                        if ($scope.ResultObtained.ObjStudConsolidatedResult.Ordinance == null || $scope.ResultObtained.ObjStudConsolidatedResult.Ordinance == "") {
                            $scope.ordinanceapplied = 'NOT APPLIED';
                        }
                        else {
                            $scope.ordinanceapplied = '*APPLIED';
                        }
                        $scope.TotalEGP = $scope.ResultObtained.ObjStudConsolidatedResult.EGP + $scope.ResultObtained.ObjStudConsolidatedResultPrevSem.EGP;
                        $scope.TotalCredits = $scope.ResultObtained.ObjStudConsolidatedResult.EarnedCredit + $scope.ResultObtained.ObjStudConsolidatedResultPrevSem.EarnedCredit;
                        $scope.ResultObtained.ObjStudConsolidatedResult.EGP = String($scope.ResultObtained.ObjStudConsolidatedResult.EGP);
                        $scope.ResultObtained.ObjStudConsolidatedResult.EGP = parseFloat($scope.ResultObtained.ObjStudConsolidatedResult.EGP).toFixed(2);
                        for (var i = 0; i < $scope.ResultObtained.ObjStudConsolidatedResult.PaperCount; ++i) {
                            //debugger;
                            $scope.ResultObtained.ObjLstPaperResultModel[i].GradePoints = String($scope.ResultObtained.ObjLstPaperResultModel[i].GradePoints);
                            $scope.ResultObtained.ObjLstPaperResultModel[i].GradePoints = parseFloat($scope.ResultObtained.ObjLstPaperResultModel[i].GradePoints).toFixed(2);
                            $scope.ResultObtained.ObjLstPaperResultModel[i].EGP = String($scope.ResultObtained.ObjLstPaperResultModel[i].EGP);
                            $scope.ResultObtained.ObjLstPaperResultModel[i].EGP = parseFloat($scope.ResultObtained.ObjLstPaperResultModel[i].EGP).toFixed(2);

                            //checking for overall absence in paper
                            if ($scope.ResultObtained.ObjLstPaperResultModel[i].IsAbsent == true) {
                                //debugger;
                                //to be done only if egp is not 0 or final grade is not f ask sir! also ask for grade points.
                                $scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade = 'ABS';
                                //$scope.ResultObtained.ObjLstPaperResultModel[i].EGP = 0;
                            }
                            if ($scope.ResultObtained.ObjLstPaperResultModel[i].IsUFM == true) {
                                //debugger;
                                $scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade = 'UFM';
                                //$scope.ResultObtained.ObjLstPaperResultModel[i].EGP = 0;
                            }


                            //debugger;
                            if ($scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade == null) {
                                $scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade = '--';
                            }

                            //if ($scope.ResultObtained.ObjLstPaperResultModel[i].EGP == "" || $scope.ResultObtained.ObjLstPaperResultModel[i].EGP == null) {
                            //    $scope.ResultObtained.ObjLstPaperResultModel[i].EGP = '--';
                            //}

                            //ordinance check
                            if ($scope.ResultObtained.ObjLstPaperResultModel[i].EarningMarks != null || $scope.ResultObtained.ObjLstPaperResultModel[i].AdhocMarks != null) {
                                $scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade = $scope.ResultObtained.ObjLstPaperResultModel[i].FinalGrade + '*';
                            }
                            //ordinance check over
                        }
                    }
                }
            })

            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            })
    }
});

