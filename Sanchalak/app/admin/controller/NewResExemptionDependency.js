app.controller('NewResExemptionDependencyCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, $window, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $scope.FPMstPaperTeachingLearningMapIdList = [];
    $scope.ResExemtion = {};
    $rootScope.pageTitle = "Result Exemption Dependency";
    
    //$localStorage.NewTLMAMATLst = [];
    $scope.AcademicYearGet = function () {

        //var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/AcademicYearGet',
            //data: data,
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
                        $scope.ResExemtion.AcademicYearId = $localStorage.Details.AcademicYearId;
                        $scope.ResExemtion.FacultyId = $localStorage.Details.FacultyId;
                        $scope.FacultyGet();
                    }
                                 
                    //else {
                    //    $scope.ResExemtion.AcademicYearId = response.obj;
                    //    $scope.FacultyGet();
                    //}
                    $scope.DoNotExemptPaperList = {};
                    $scope.TLMAMATList = {};


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
            url: 'api/ResExemptionDependency/FacultyGet',
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
                    if($scope.ResExemtion.FacultyId == $localStorage.Details.FacultyId)
                    {
                        if ($localStorage.Details.ProgrammeInstanceId) {

                            $scope.ResExemtion.ProgrammeInstanceId = $localStorage.Details.ProgrammeInstanceId;
                            //alert($localStorage.Details.ProgrammeInstanceId);
                            $scope.getProgrammeInstanceListByAcadId();

                        }
                    }
                    else {
                        //$localStorage.Details.ProgrammeInstanceId = null;
                        //$localStorage.Details.SpecialisationId = null;
                        //$localStorage.Details.ProgrammeInstancePartId = null;
                        //$localStorage.Details.ProgrammePartTermId = null;
                        //$localStorage.Details.FailPaperId = null;
                        $localStorage.Details = {};
                    }
                    

                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.getProgrammeInstanceListByAcadId = function () {
      
        
        $scope.InstList = {};
        $scope.SpecialisationList = {};
        $scope.ProgPartList = {};
        $scope.ProgPartTermList = {};
        $scope.PaperList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ResExemtion,
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
                    if ($scope.ResExemtion.ProgrammeInstanceId == $localStorage.Details.ProgrammeInstanceId) {
                        if ($localStorage.Details.SpecialisationId) {
                           
                            $scope.ResExemtion.SpecialisationId = $localStorage.Details.SpecialisationId;
                            $scope.getSpecialisationListByProgInstanceId();
                        }
                    }
                    
                   else {
                        $localStorage.Details = {};
                    }


                    //$scope.DoNotExemptPaperList = {};
                    //$scope.TLMAMATList = {};
                    //$scope.PaperList = {};
                    //$scope.ProgPartTermList = {};
                    //$scope.ProgPartList = {};
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.getSpecialisationListByProgInstanceId = function () {
        
        $scope.SpecialisationList = {};
        $scope.ProgPartList = {};
        $scope.ProgPartTermList = {};
        $scope.PaperList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/GetSpecialisationbyProgrammeInstanceId',
            data: $scope.ResExemtion,
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
                        $scope.SpecialisationList = {};

                    }
                }
                else {
                    $scope.SpecialisationList = response.obj;

                    $scope.getProgrammePartListByProgInstId();
                    if ($scope.ResExemtion.SpecialisationId == $localStorage.Details.SpecialisationId) {

                        if ($localStorage.Details.ProgrammePartId) {

                            $scope.ResExemtion.ProgrammePartId = $localStorage.Details.ProgrammePartId;
                            $scope.getProgrammePartListByProgInstId();

                        }
                    }
                    
                    else {
                        $localStorage.Details = {};
                    }

                    
                    /*$scope.DoNotExemptPaperList = {};*/
                    //$scope.TLMAMATList = {};
                    //$scope.PaperList = {};
                    //$scope.ProgPartTermList = {};
                    //$scope.ProgPartList = {};
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.getProgrammePartListByProgInstId = function () {
        $scope.NewTLMAMATLst = {};
        $scope.ProgPartList = {};
        $scope.ProgPartTermList = {};
        $scope.PaperList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/ProgrammePartGetByProgInstId',
            data: $scope.ResExemtion,
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
                    
                    if ($scope.ResExemtion.ProgrammePartId == $localStorage.Details.ProgrammePartId) {
                        if ($localStorage.Details.ProgrammePartTermId) {

                            $scope.ResExemtion.ProgrammePartTermId = $localStorage.Details.ProgrammePartTermId;
                            $scope.getProgPartTermListByProgInstPartId();
                        }
                    }
                    else {
                        $localStorage.Details = {};
                    }
                    //$scope.DoNotExemptPaperList = {};
                    $scope.NewTLMAMATLst = {};
                    $scope.PaperList = {};
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgPartTermListByProgInstPartId = function () {
        
        $scope.ProgPartTermList = {};
        $scope.PaperList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/ProgrammePartTermGetByProgInstId',
            data: $scope.ResExemtion,
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
                   // 
                    $scope.ProgPartTermList = response.obj;
                    if ($scope.ResExemtion.ProgrammePartTermId == $localStorage.Details.ProgrammePartTermId) {

                        if ($localStorage.Details.FailPaperId) {

                            $scope.ResExemtion.FailPaperId = $localStorage.Details.FailPaperId;
                            $scope.getFailPaperListbyPTId();
                        }
                    }
                    else {
                        $localStorage.Details = {};
                    }

                    
                    //$localStorage.ProgrammePartTermId = $scope.ProgPartTermList[0].ProgrammePartTermId
                    //$scope.DoNotExemptPaperList = {};
                    //$scope.NewTLMAMATLst = {};
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFailPaperListbyPTId = function () {
        
        
        $scope.PaperList = {};
        $scope.NewTLMAMATLst = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/PaperListbyProgrammePartTermId',
            data: $scope.ResExemtion,
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
                        $scope.PaperList = {};

                    }
                }
                else {
                    
                    $scope.PaperList = response.obj;

                    if ($localStorage.Details.FailPaperId) {
                        
                        $scope.ResExemtion.FailPaperId = $localStorage.Details.FailPaperId;
                        $scope.getTLMAMATByPaperId();
                    }
                    $scope.DoNotExemptPaperList = {};
                    $scope.TLMAMATList = {};
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getTLMAMATByPaperId = function () {
        
        $scope.TLMAMATList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionDependency/TLMAMATbyPaperId',
            data: $scope.ResExemtion,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else if (response.response_code == "201") {
                    alert(response.obj);
                    //$rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else if (response.response_code == "404") {

                    //$scope.TLMAMATList = {};
                }
                else {
                    
                    $scope.TLMAMATList = response.obj;
                    
                   // $localStorage.FPMstPaperTeachingLearningMapIdList = {};
                        $localStorage.NewTLMAMATLst = [];
                        NewTLMAMATLst = [];
                        TLMAMATCount = 0;
                        for (key in Object.keys($scope.TLMAMATList)) {
                            if ($scope.TLMAMATList[key].TLMAMATChecked == true) {

                                TLMAMATCount = TLMAMATCount + 1;
                                NewTLMAMATLst.push($scope.TLMAMATList[key]);
                                /*$scope.getDoNotExemptPaperListbyPTANDTLMAMATId($scope.TLMAMATList[key]);*/
                            }
                            else {
                                $localStorage.NewTLMAMATLst = [];
                            }
                        };

                        $localStorage.NewTLMAMATLst = NewTLMAMATLst;
                        //New Start
                    if ($localStorage.NewTLMAMATLst != null || $localStorage.NewTLMAMATLst != undefined) {
                        //;
                            for (var a = 0; $localStorage.NewTLMAMATLst.length > a; a++) {
                                $localStorage.NewTLMAMATLst[a].TLMAMAT;
                                //$scope.CheckChangeTLMAMAT($localStorage.NewTLMAMATLst[a], $localStorage.NewTLMAMATLst[a].TLMAMATChecked);
                            }
                        }
                        //New End
                        $localStorage.ResExemtion = {};
                        $localStorage.ResExemtion = $scope.ResExemtion;
                    

                    
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.AtLeastOneCheckforResExemdepen = function (a) {
        
        var count = 0;
        for (var i = 0; i < $scope.TLMAMATList.length; i++) {
            if ($scope.TLMAMATList[i].TLMAMATChecked == null || $scope.TLMAMATList[i].TLMAMATChecked === undefined
                || $scope.TLMAMATList[i].TLMAMATChecked == false
            ) {
                continue;

            }
            else {
                count = count + 1;
            }
        }
        if (count > 0) {
            
           // $scope.TLMAMATId = a;
            $localStorage.testStorage = "hello";
            $localStorage.Details = {};
            $localStorage.Details.AcademicYearId = $scope.ResExemtion.AcademicYearId;
            $localStorage.Details.FacultyId = $scope.ResExemtion.FacultyId;
            $localStorage.Details.ProgrammeInstanceId = $scope.ResExemtion.ProgrammeInstanceId;
            $localStorage.Details.ProgrammePartId = $scope.ResExemtion.ProgrammePartId;
            $localStorage.Details.SpecialisationId = $scope.ResExemtion.SpecialisationId;
            $localStorage.Details.ProgrammePartTermId = $scope.ResExemtion.ProgrammePartTermId;
            $localStorage.Details.FailPaperId = $scope.ResExemtion.FailPaperId;
            //$localStorage.Details.FPMstPaperTeachingLearningMapId = $scope.ResExemtion.FPMstPaperTeachingLearningMapId;
            //$localStorage.Details.FPMstPaperTeachingLearningMapIdList = $scope.ResExemtion.FPMstPaperTeachingLearningMapIdList;
            $localStorage.Details.TLMAMATName = $scope.TLMAMATList[0].TLMAMATName;
            $localStorage.Details.PaperName = $scope.PaperList.find(x => x.FailPaperId === $scope.ResExemtion.FailPaperId).PaperName;
            $localStorage.MFPaperName = $localStorage.Details.PaperName;
            
            $state.go('NewResExemptionDependency1');
            //$scope.getDoNotExemptPaperListbyPTANDTLMAMATId();
        }
        else {
            alert("Please Select at least one checkbox");
        }

    };

    $scope.getDoNotExemptPaperListbyPTANDTLMAMATId = function () {
            $http({
                method: 'POST',
                url: 'api/ResExemptionDependency/PaperListbyProgrammePartTermIdandTLMAMATId',
                data: { FailPaperId: $localStorage.ResExemtion.FailPaperId, ProgrammePartTermId: $localStorage.ResExemtion.ProgrammePartTermId, SpecialisationId:$localStorage.ResExemtion.SpecialisationId},
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

                            $scope.DoNotExemptPaperList = {};


                        }
                    }
                    else {

                        $scope.DoNotExemptPaperList = {};
                       
                        $scope.DoNotExemptPaperList = response.obj;
                        $scope.DoNotExemptPaperListNew = [];
                        
                        /*$scope.ItemNew = {ProgrammePartTermId: 8, FPMstPaperTeachingLearningMapId: 12, DonotExemptPaperId: 11};*/
                        /*alert($localStorage.FPMstPaperTeachingLearningMapIdList);*/
                        var z = 0;
                        var j;
                        ;
                        for (j = 0; j < $scope.DoNotExemptPaperList.length; j++) {
                            for (var i = 0; i < $localStorage.FPMstPaperTeachingLearningMapIdList.length; i++) {
                                //   $scope.DoNotExemptPaperListNew[z].ProgrammePartTermId = $scope.DoNotExemptPaperList[j].ProgrammePartTermId;
                                $scope.ItemNew = {};
                                //;
                                //  $scope.DoNotExemptPaperListNew[z].TLMAMAPP = $localStorage.FPMstPaperTeachingLearningMapIdList[i];
                                $scope.ItemNew.ProgrammePartTermId = $localStorage.Details.ProgrammePartTermId;
                                $scope.ItemNew.FPMstPaperTeachingLearningMapId = $localStorage.FPMstPaperTeachingLearningMapIdList[i].FPMstPaperTeachingLearningMapId;
                                $scope.ItemNew.TLMAMATChecked = $localStorage.FPMstPaperTeachingLearningMapIdList[i].TLMAMATChecked;
                                $scope.ItemNew.DonotExemptPaperId = $scope.DoNotExemptPaperList[j].DonotExemptPaperId;
                                $scope.ItemNew.DExPMstPaperTeachingLearningMapId = $scope.DoNotExemptPaperList[j].DExPMstPaperTeachingLearningMapId;
                                $scope.ItemNew.FailPaperId = $localStorage.ResExemtion.FailPaperId;
                                //$scope.ItemNew.DoNotExemptPaperChecked = $scope.DoNotExemptPaperList[j].DoNotExemptPaperChecked;

                                $scope.DoNotExemptPaperListNew.push($scope.ItemNew);
                                console.log(j + " " + $scope.DoNotExemptPaperList[j].DonotExemptPaperId);

                                z++;

                                // FPMstPaperTeachingLearningMapIdList.FPMstPaperTeachingLearningMapId

                            }



                        }

                      
                        //  $scope.DoNotExemptPaperList.TLMAMMAP = $localStorage.FPMstPaperTeachingLearningMapIdList;
                        /*$scope.NewDoNotExemptPaperLst = {};*/
                        NewDoNotExemptPaperLst = [];
                        
                        DoNotExemptPaperCount = 0;
                        for (key in Object.keys($scope.DoNotExemptPaperList)) {
                            if ($scope.DoNotExemptPaperList[key].DoNotExemptPaperChecked) {

                                DoNotExemptPaperCount = DoNotExemptPaperCount + 1;
                                NewDoNotExemptPaperLst.push($scope.DoNotExemptPaperList[key]);
                            }
                            else {
                                $scope.NewDoNotExemptPaperLst = {};
                            }
                        };

                        $scope.NewDoNotExemptPaperLst = NewDoNotExemptPaperLst;
                        console.log("=============$scope.DoNotExemptPaperList================");
                        console.log($scope.DoNotExemptPaperList);
                        /*alert($scope.FPMstPaperTeachingLearningMapIdList);*/
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
     
    };

    $scope.CheckChangeTLMAMAT = function (TLMAMAT, checked) {
        
        //$scope.FPMstPaperTeachingLearningMapId = TLMAMAT.FPMstPaperTeachingLearningMapId;
        
        //$state.go('NewResExemptionDependency1');
        if (checked) {
            ;
            var TLMAMATIndex = NewTLMAMATLst.map(function (item) { return item.FPMstPaperTeachingLearningMapId; }).indexOf(TLMAMAT.FPMstPaperTeachingLearningMapId);

            if (TLMAMATIndex >= 0) {
                TLMAMAT.TLMAMATChecked = true;
                TLMAMATCount = TLMAMATCount + 1;
                $scope.FPMstPaperTeachingLearningMapIdList.push({ 'FPMstPaperTeachingLearningMapId': TLMAMAT.FPMstPaperTeachingLearningMapId, 'TLMAMATChecked': TLMAMAT.TLMAMATChecked });
                
               $localStorage.FPMstPaperTeachingLearningMapIdList = $scope.FPMstPaperTeachingLearningMapIdList;
                //$scope.getDoNotExemptPaperListbyPTANDTLMAMATId();
            }
            else if (TLMAMATIndex < 0) {
                debugger;
                TLMAMAT.TLMAMATChecked = true;
                TLMAMATCount = TLMAMATCount + 1;
                NewTLMAMATLst.push(TLMAMAT);
                $scope.FPMstPaperTeachingLearningMapIdList.push({ 'FPMstPaperTeachingLearningMapId': TLMAMAT.FPMstPaperTeachingLearningMapId, 'TLMAMATChecked': TLMAMAT.TLMAMATChecked });
                ;
               $localStorage.FPMstPaperTeachingLearningMapIdList = $scope.FPMstPaperTeachingLearningMapIdList;

               
            }

        }
        else if (!(checked)) {
            ;
            var TLMAMATIndex1 = NewTLMAMATLst.map(function (item) { return item.FPMstPaperTeachingLearningMapId; }).indexOf(TLMAMAT.FPMstPaperTeachingLearningMapId);
                /* NewTLMAMATLst[TLMAMATIndex1].TLMAMATChecked = false;*/

            TLMAMAT.TLMAMATChecked = false;
            //for (var x = 0; $localStorage.FPMstPaperTeachingLearningMapIdList.length > x;x++) {
                $localStorage.FPMstPaperTeachingLearningMapIdList[TLMAMATIndex1].TLMAMATChecked = false;
            //}
            $localStorage.FPMstPaperTeachingLearningMapIdList.splice(TLMAMATIndex1,1);
            
        }
        /*$localStorage.NewTLMAMATLst = {};*/
        $localStorage.NewTLMAMATLst = NewTLMAMATLst;
        
    };

    $scope.DisplayMFPaperName = $localStorage.MFPaperName;
    $scope.NewTLMAMATLstGet = $localStorage.NewTLMAMATLst;

    $scope.CheckChangeDoNotExemptPaper = function (DoNotExemptPaper, checked) {


        if (checked) {
            var DoNotExemptPaperIndex = NewDoNotExemptPaperLst.map(function (item) { return item.DExPMstPaperTeachingLearningMapId; }).indexOf(DoNotExemptPaper.DExPMstPaperTeachingLearningMapId);

            if (DoNotExemptPaperIndex >= 0) {
                DoNotExemptPaper.DoNotExemptPaperChecked = true;
                DoNotExemptPaperCount = DoNotExemptPaperCount + 1;
            }
            else if (DoNotExemptPaperIndex < 0) {
                DoNotExemptPaper.DoNotExemptPaperChecked = true;
                DoNotExemptPaperCount = DoNotExemptPaperCount + 1;
                NewDoNotExemptPaperLst.push(DoNotExemptPaper);

            }

        }
        else if (!(checked)) {

            var DoNotExemptPaperIndex1 = NewDoNotExemptPaperLst.map(function (item) { return item.DExPMstPaperTeachingLearningMapId; }).indexOf(DoNotExemptPaper.DExPMstPaperTeachingLearningMapId);

            NewDoNotExemptPaperLst[DoNotExemptPaperIndex1].DoNotExemptPaperChecked = false;
            /*DoNotExemptPaperCount = DoNotExemptPaperCount - 1;*/

        }
        $scope.NewDoNotExemptPaperLst = {};
        $scope.NewDoNotExemptPaperLst = NewDoNotExemptPaperLst;

    };

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    

    $scope.addResExemptionDependency = function () {
       
        debugger;
        $scope.DoNotExemptPaperListNew = [];
       
        
        var z = 0;
        var j;
        for (j = 0; j < $scope.DoNotExemptPaperList.length; j++)
        {
            for (var i = 0; i < $localStorage.NewTLMAMATLst.length; i++)
            {
             //   $scope.DoNotExemptPaperListNew[z].ProgrammePartTermId = $scope.DoNotExemptPaperList[j].ProgrammePartTermId;
                $scope.ItemNew = {};
               
              //  $scope.DoNotExemptPaperListNew[z].TLMAMAPP = $localStorage.FPMstPaperTeachingLearningMapIdList[i];
                $scope.ItemNew.AcademicYearId = $localStorage.Details.AcademicYearId;
                $scope.ItemNew.ProgrammePartTermId = $localStorage.Details.ProgrammePartTermId;
                $scope.ItemNew.SpecialisationId = $localStorage.Details.SpecialisationId;
                $scope.ItemNew.FPMstPaperTeachingLearningMapId = $localStorage.NewTLMAMATLst[i].FPMstPaperTeachingLearningMapId;
                $scope.ItemNew.TLMAMATChecked = $localStorage.NewTLMAMATLst[i].TLMAMATChecked;
                $scope.ItemNew.DonotExemptPaperId = $scope.DoNotExemptPaperList[j].DonotExemptPaperId;
                $scope.ItemNew.DExPMstPaperTeachingLearningMapId = $scope.DoNotExemptPaperList[j].DExPMstPaperTeachingLearningMapId;
                $scope.ItemNew.FailPaperId = $localStorage.ResExemtion.FailPaperId;
                $scope.ItemNew.DoNotExemptPaperChecked = $scope.DoNotExemptPaperList[j].DoNotExemptPaperChecked;

                $scope.DoNotExemptPaperListNew.push($scope.ItemNew);
                    console.log(j + " " + $scope.DoNotExemptPaperList[j].DonotExemptPaperId);
                
                z++;

               // FPMstPaperTeachingLearningMapIdList.FPMstPaperTeachingLearningMapId

            }



        }
        
        $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/ResExemptionDependency/ResExemptionDependencyAdd',
                    data: $scope.DoNotExemptPaperListNew,
                    eaders: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                       
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            ;
                            //$localStorage.FPMstPaperTeachingLearningMapIdList = "";
                           // $localStorage.FPMstPaperTeachingLearningMapIdList = {};
                            alert(response.obj);
                            $scope.offSpinner();

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
               };

    $scope.deleteResExemptionDependency = function (ev) {

        
        
        debugger;
        
        if ($scope.DoNotExemptPaperList == null || $scope.DoNotExemptPaperList === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResExemption')))
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

                
                
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/ResExemptionDependency/ResExemptionDependencyDelete',
                    data: $scope.DoNotExemptPaperListNew,
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
                                $scope.offSpinner();
                                $scope.DoNotExemptPaperList = {};
                                $scope.NewTLMAMATLst = {};

                                //$scope.getTLMAMATByPaperId();

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

    $scope.backToList = function () {
     
        if ($localStorage.Details) {
            

           // for (var i = 0; i < $localStorage.FPMstPaperTeachingLearningMapIdList.length; i++) {
           //     $localStorage.FPMstPaperTeachingLearningMapIdList[i].FPMstPaperTeachingLearningMapId = null;
           //     //$localStorage.$reset(FPMstPaperTeachingLearningMapIdList[i].FPMstPaperTeachingLearningMapId);
           //     //$window.$localStorage.FPMstPaperTeachingLearningMapIdList[i].FPMstPaperTeachingLearningMapId=null;
                
           // }
           ///* $localStorage.Details = $localStorage.FPMstPaperTeachingLearningMapIdList[i].FPMstPaperTeachingLearningMapId;*/
            //$scope.ResExemtion = $localStorage.Details;
            $scope.ResExemtion = {};
            $scope.ResExemtion.AcademicYearId = $localStorage.Details.AcademicYearId;
            $scope.ResExemtion.FacultyId = $localStorage.Details.FacultyId;
            $scope.ResExemtion.ProgrammeInstanceId = $localStorage.Details.ProgrammeInstanceId;
            $scope.ResExemtion.ProgrammePartId = $localStorage.Details.ProgrammePartId;
            $scope.ResExemtion.SpecialisationId = $localStorage.Details.SpecialisationId;
            //$scope.ResExemtion.ProgrammePartTermId = $localStorage.Details.ProgrammePartTermId;
            //$localStorage.FPMstPaperTeachingLearningMapIdList = {};
        }
        //$localStorage.FPMstPaperTeachingLearningMapIdList = {};
        $state.go('ResExemptionDependency');
        
    };

    $scope.ClearSelection = function () {
        
        $localStorage.Details = {};
        $scope.ResExemtion = {};
        $localStorage.NewTLMAMATLst = [];
        $scope.TLMAMATList = {};
        

    };


    













});