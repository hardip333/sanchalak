app.controller('PreProgrammeInstancePartTermCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
    $scope.ProgInstList = [];
    $scope.ProgInst = {};
    //$scope.ProgInstPartTermTableparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.getProgInstList
    //});

    $scope.clearlocalstorageterm = function () {
       // alert("Clear Part Term");
        //debugger;

        $localStorage.PreProgInstData.ProgrammeId = null;
        $localStorage.PreProgInstData.AcademicYearId = null;
        $localStorage.localObj = {};
        //debugger;
        $scope.ProgInst = {};

        // $rootScope.ProgInst = {};
        //$localStorage.PreProgInstData.AcademicYearId = null;
        // $scope.ProgInst.AcademicYearId = "";
        // alert("test" + $scope.ProgInst.AcademicYearId);

        //  $scope.AcadList = "";
        // $scope.InstanceNameList = {};
        // $scope.ProgPartList = {};
        //debugger;
        //$scope.ProgInst.ProgrammePartId = null;
        //$scope.getAcademicList();
        //$scope.getgetInstanceNameList();

        //$scope.ProgInst.AcademicYearId = null;
        //$scope.ProgInst.ProgrammePartId = null;
        //$scope.ProgInst.ProgrammeId = null;
        //$scope.ProgInst.ProgrammeInstanceId = null;  

        /*--This is the flag for Enable selected Part Term value--*/
        $scope.partTermDisable = false;
    };


    if ($localStorage.localObj.flagfromPart == 1) {
        /*--This is the flag for Disable selected Part Term value--*/
        $scope.partTermDisable = true;
        /*--This is the flag for identify user move from Part--*/
        $localStorage.localObj.flagfromPart = 0;

        //$scope.ProgInst.AcademicYearId = $localStorage.PreProgInstData.AcademicYearId;
        //$scope.ProgInst.ProgrammePartId = $localStorage.localObj.ProgrammePartId;
        //$scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
        //$scope.ProgInst.ProgrammeInstanceId = $localStorage.localObj.ProgrammeInstance;  

    }
    else {
    
        $localStorage.PreProgInstData.ProgrammeId = null;
        $localStorage.PreProgInstData.AcademicYearId = null;
        $localStorage.localObj = {};
        $scope.ProgInst = {};

        /*--This is the flag for Enable selected Part Term value--*/
        $scope.partTermDisable = false;
    }


    $scope.resetProgInstPartTerm = function () {
        $scope.ProgInst = {};
    };


/*
    $scope.getProgrammeList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgrmLst = function () {
        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList1 = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgPartTermList = function () {
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammePartTermGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermList1 = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;
                $scope.getBranchListByProgrammeId();
                $scope.getProgrammePartListByProgrammeId();

            })
            .error(function (res) {
                alert(res);
            });
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];  
               // $scope.Faculty = response.obj; // Krunal's code               
                $scope.ProgInst.FacultyId = $scope.Institute.Id;
                $scope.ProgInst.InstituteId = $scope.Institute.InstituteId;
                $scope.getPreProgrammeList();
                $scope.getPreProgInstPartTermListByFacultyId();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                if ($localStorage.PreProgInstData.AcademicYearId != null) {

                    $scope.getgetInstanceNameList();
                }

                $scope.ProgInst.AcademicYearId = $localStorage.PreProgInstData.AcademicYearId;

            })
            .error(function (res) {
                alert(res);
            });
    };
/*-------------------Pre Admission Configuration Module End----------------------------*/

 /*   $scope.getProgInstList = function () {

        var data = new Object();
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProgInstPartTermTableparam = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.getPreProgInstPartTermListByFacultyId = function () {

       var facultyId= { FacultyId: $scope.Institute.Id }
        // $scope.ProgInst.FacultyId = $scope.Faculty.Id;
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PreProgPartTermGetByFacultyId',
            data: facultyId
            ,
            //data: { FacultyId: $scope.Faculty.Id },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //$scope.ProgInstPartTermListByFacultyId = response.obj;
                    $scope.ProgInstPartTermByFacultyIdTableparam = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.FindProgIdPre = function () {
        //debugger;
        if ($scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId == undefined) {
            for (key of Object.keys($scope.InstanceNameList)) {
                if ($scope.InstanceNameList[key].Id == $scope.ProgInst.ProgrammeInstanceId) {
                    ProgId = $scope.InstanceNameList[key].ProgrammeId;
                }
            }
            //$scope.ProgInst.ProgrammeId;
            $scope.ProgInst.ProgrammeId = ProgId;
        }
    };

    $scope.getBranchListByProgrammeId = function () {

        if ($localStorage.PreProgInstData.ProgrammeId == null || $localStorage.PreProgInstData.ProgrammeId == undefined) {

            $scope.FindProgIdPre();
        }
        else {
            $scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
        }
        //for (key of Object.keys($scope.InstanceNameList)) {
        //    if ($scope.InstanceNameList[key].Id == $scope.ProgInst.ProgrammeInstanceId) {
        //        ProgId = $scope.InstanceNameList[key].ProgrammeId;
        //    }
        //}
        //$scope.ProgInst.ProgrammeId = ProgId;


        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BranchList = {};
                }
                else {
                $scope.BranchList = response.obj;
                //$scope.ProgInst.SpecialisationId = $localStorage.localObj.ProgrammeInstance.SpecialisationId;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };
/*-------------------Pre Admission Configuration Module End----------------------------*/

  /*  $scope.getBranchList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList1 = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.getProgrammePartListByProgrammeId = function () {
        if ($localStorage.PreProgInstData.ProgrammeId == null || $localStorage.PreProgInstData.ProgrammeId == undefined) {

            $scope.FindProgIdPre();
           // ProgInst.ProgrammeInstanceId
        }
        else {
            $scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
        }
        //for (key of Object.keys($scope.InstanceNameList)) {
        //    if ($scope.InstanceNameList[key].Id == $scope.ProgInst.ProgrammeInstanceId) {
        //        ProgId = $scope.InstanceNameList[key].ProgrammeId;
        //        console.log("199", ProgId);
        //    }
        //}
        ////$scope.ProgInst.ProgrammeId;
        //$scope.ProgInst.ProgrammeId = ProgId;
        console.log($scope.ProgInst);

        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                $scope.ProgPartList = response.obj;
                    if ($localStorage.localObj.ProgrammePartId != null)
                    {                      
                $scope.getProgPartTermListByPartId();
                    }
                    $scope.ProgInst.ProgrammePartId = $localStorage.localObj.ProgrammePartId;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgPartTermListByPartId = function () {
        //$scope.ProgInst.ProgrammePartId = $localStorage.ProgrammeInstancePart;
		//console.log("=========================================");
		//console.log($scope.ProgInst);
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammePartTermGetByPartId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartTermList = {};
                }
                else {
                $scope.ProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };
/*-------------------Pre Admission Configuration Module End----------------------------*/

 /*   $scope.getProgrammePartList = function () {
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartList1 = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.getPreProgrammeList = function () {
        // $scope.ProgInst.FacultyId = $scope.Faculty.Id;
        var InstituteId = { InstituteId: $scope.Institute.InstituteId }
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeListGet',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList = response.obj;
                $scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
                $scope.getBranchListByProgrammeId();
                $scope.getProgrammePartListByProgrammeId();
               
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getgetInstanceNameList = function () {
        //  $scope.ProgInst.FacultyId = $scope.Faculty.Id;
        $scope.ProgInst.InstituteId = $scope.Institute.InstituteId;
        $http({
            method: 'POST',
            //url: 'api/MstProgramInstance/PreMstProgrammeInstanceListGetbyFacIdAndYearId',
            url: 'api/MstProgramInstance/GetMstProgrammebyFacIdAndYearId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    $scope.InstanceNameList = {};
                }
                else {
                    //alert("Change Called");
                  //  console.log("======", response.obj);
                    $scope.InstanceNameList = response.obj;
                    $scope.ProgInst.ProgrammeInstanceId = $localStorage.localObj.ProgrammeInstance;
                    $scope.getBranchListByProgrammeId();
                    $scope.getProgrammePartListByProgrammeId();
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };
/*-------------------Pre Admission Configuration Module End----------------------------*/

  /*  $scope.progInstPartTermAdd = function () {

        if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
            || $scope.ProgInst.ProgrammePartTermId == null || $scope.ProgInst.ProgrammePartTermId === undefined
            || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
            || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined
            || $scope.ProgInst.MaxPapers == null || $scope.ProgInst.MaxPapers === undefined
            || $scope.ProgInst.MinPapers == null || $scope.ProgInst.MinPapers === undefined
            || $scope.ProgInst.IsSeparatePassingHead == null || $scope.ProgInst.IsSeparatePassingHead === undefined) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermAdd',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        //$scope.getProgInstList();
                        $scope.getPreProgInstPartTermListByFacultyId();
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.preProgInstPartTermAdd = function () {

        if ($scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
            || $scope.ProgInst.ProgrammePartTermId == null || $scope.ProgInst.ProgrammePartTermId === undefined
           // || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
           // || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined
            || $scope.ProgInst.MaxPapers == null || $scope.ProgInst.MaxPapers === undefined
            || $scope.ProgInst.MinPapers == null || $scope.ProgInst.MinPapers === undefined
            // || $scope.ProgInst.IsSeparatePassingHead == null || $scope.ProgInst.IsSeparatePassingHead === undefined
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            if ($scope.ProgInst.MaxMarks1 == null || $scope.ProgInst.MaxMarks1 === undefined) { $scope.ProgInst.MaxMarks1 = null; }
            if ($scope.ProgInst.MinMarks1 == null || $scope.ProgInst.MinMarks1 === undefined) { $scope.ProgInst.MinMarks1 = null; }
            $scope.ProgInst.FacultyId = $scope.Institute.Id;
            $http({
                method: 'POST',
                url: 'api/MstProgrammeInstancePartTerm/PreProgrammeInstancePartTermAdd',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        //alert(response.obj);
                        alert(response.obj.strMessage);
                        //alert(response.obj.ProgInstancePartTerm);
                        if (response.obj.ProgInstancePartTerm > 0) {
                            if ($localStorage.localObj) {
                                $localStorage.localObj.ProgrammeInstancePartTerm = response.obj.ProgInstancePartTerm;
                                $localStorage.localObj.ProgInstanceName = response.obj.ProgInstanceName;
                                $localStorage.localObj.ProgrammeName = response.obj.ProgrammeName;
                                $localStorage.localObj.PartName = response.obj.ProgrammePartName;
                                $localStorage.localObj.ProgrammePartTermName = response.obj.ProgrammePartTermName;
                                /*--This is the flag for identify user move from Part--*/
                                // $localStorage.localObj.flagfromPart = 0;
                            } else {
                                $localStorage.localObj = {};
                                $localStorage.localObj.ProgrammeInstancePartTerm = response.obj.ProgInstancePartTerm;
                                $localStorage.localObj.ProgInstanceName = response.obj.ProgInstanceName;
                                $localStorage.localObj.ProgrammeName = response.obj.ProgrammeName;
                                $localStorage.localObj.PartName = response.obj.ProgrammePartName;
                                $localStorage.localObj.ProgrammePartTermName = response.obj.ProgrammePartTermName;
                                /*--This is the flag for identify user move from Part--*/
                                // $localStorage.localObj.flagfromPart = 0;
                            }

                            $scope.ProgInst = {};
                            //$scope.getProgInstList();
                            $scope.getPreProgInstPartTermListByFacultyId();
                            $state.go('AdmEligibilityGroupAdd');
                        }
                        else {
                            $scope.ProgInst = {};
                        }
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.modifyProgInstPartTermdata = function (data) {

        $scope.showFormFlag = true;
        $scope.ProgInst = data;

        if ($localStorage.PreProgInstData == null || $localStorage.PreProgInstData == undefined) {

            $scope.FindProgIdPre();
        }
        else {
            $scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
        }
        $localStorage.localObj.ProgrammeInstance = $scope.ProgInst.ProgrammeInstanceId;
        $localStorage.localObj.ProgrammePartId = $scope.ProgInst.ProgrammePartId;

        $scope.getgetInstanceNameList();
        // $scope.getBranchListByProgrammeId();
        //// $scope.getBranchList();
        // $scope.getProgrammePartListByProgrammeId();
        // $scope.getProgPartTermListByPartId();
        // //$scope.getProgPartTermList();
        // $scope.getPreProgInstPartTermListByFacultyId();
    };

    $scope.modifyProgInstPartTerm = function () {

        if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
            || $scope.ProgInst.ProgrammePartTermId == null || $scope.ProgInst.ProgrammePartTermId === undefined
            //|| $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
            //|| $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined
            || $scope.ProgInst.MaxPapers == null || $scope.ProgInst.MaxPapers === undefined
            || $scope.ProgInst.MinPapers == null || $scope.ProgInst.MinPapers === undefined
            || $scope.ProgInst.IsSeparatePassingHead == null || $scope.ProgInst.IsSeparatePassingHead === undefined) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeInstancePartTerm/PreProgrammeInstancePartTermUpdate',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                        $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    } else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        $scope.showFormFlag = false;
                        //$scope.getProgInstList();
                        $scope.getPreProgInstPartTermListByFacultyId();
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteProgInstPartTerm = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgInst = data;

            $http({
                method: 'POST',
                url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermDelete',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        //$scope.getProgInstList();
                        $scope.getPreProgInstPartTermListByFacultyId();
                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }, function () {
            $scope.status = 'You decided to keep your debt.';
            alert($scope.status);
        });
    };

    $scope.showProgInstPartTerm = function (data) {
        // data.createdById = $rootScope.id;
        $scope.newProgInst = data;
        // $scope.InactiveFlag = true;
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermIsActiveEnable',
            data: $scope.newProgInst,
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
                    //$scope.getProgInstList();
                    $scope.getPreProgInstPartTermListByFacultyId();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });


    };

    $scope.hideProgInstPartTerm = function (data) {

        $scope.newProgInst = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermIsActiveDisable',
            data: $scope.newProgInst,
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
                    //$scope.getProgInstList();
                    $scope.getPreProgInstPartTermListByFacultyId();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
/*-------------------Pre Admission Configuration Module End----------------------------*/

  /*  $scope.examinationPatternGet = function () {

        $http({
            method: 'GET',
            url: 'api/MstExaminationPattern/MstExaminationPatternGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.examinationPatternTableParams = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.getPreProgInsPartTermList = function () {
        $scope.ProgInst.FacultyId = $scope.Institute.Id;
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PreProgrammeInstancePartTermGet',
            data: $scope.ProgInst,
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
                        // alert(response.obj);
                        if ($localStorage.localObj) {
                            $localStorage.localObj.ProgrammeInstancePartTerm = response.obj[0].Id;
                            $localStorage.localObj.ProgInstanceName = response.obj[0].InstanceName;
                            $localStorage.localObj.ProgrammeName = response.obj[0].ProgrammeName;
                            $localStorage.localObj.PartName = response.obj[0].PartName;
                            $localStorage.localObj.ProgrammePartTermName = response.obj[0].ProgrammePartTermName;
                        } else {
                            $localStorage.localObj = {};
                            $localStorage.localObj.ProgrammeInstancePartTerm = response.obj[0].Id;
                            $localStorage.localObj.ProgInstanceName = response.obj[0].InstanceName;
                            $localStorage.localObj.ProgrammeName = response.obj[0].ProgrammeName;
                            $localStorage.localObj.PartName = response.obj[0].PartName;
                            $localStorage.localObj.ProgrammePartTermName = response.obj[0].ProgrammePartTermName;
                        }
                        if ($localStorage.localObj.ProgrammeInstancePartTerm == undefined) {
                            alert("Data not exits");
                        }
                        else {
                            $state.go('PreConfiguration');
                            // $scope.g1();
                        }

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });


    };
/*-------------------Pre Admission Configuration Module End----------------------------*/

/*    $scope.getPreProgInsName = function () {
        $scope.ProgrammeInstanceName = $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;
        $state.go('PreConfiguration');
    };*/
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.cancelPreProgInsPartTermList = function () {
        $scope.ProgInst = {};
        $scope.showFormFlag = false;
    }
/*-------------------Pre Admission Configuration Module End----------------------------*/
 /*   $scope.nextAdd = function () {
        $state.go('AdmEligibilityGroupEdit');
    };

    $scope.newProgInstPartTermAdd = function () {
        $state.go('ProgrammeInstancePartTermAdd');
    };
    */
/*-------------------Pre Admission Configuration Module Start----------------------------*/
    $scope.backToList = function () {
        $state.go('PreProgInstancePartTermConfigEdit');
    };

    $scope.displayProgInstPartTerm = function (data) {
        $scope.ProgInst = data;
    };

    $scope.newPreProgInstPartTermConfigAdd = function () {
        $state.go('PreProgInstancePartTermConfigAdd');
    };
/*-------------------Pre Admission Configuration Module End----------------------------*/
 /*   $scope.nextbtnEligibilityGroup = function () {
        $state.go('AdmEligibilityGroupEdit');
    };

    $scope.nextbtnEligibilityGroupComponent = function () {
        $state.go('AdmEligibilityGroupComponentEdit');
    };

    $scope.nextbtnProgrammeAddOn = function () {
        $state.go('AdmProgrammeAddOnCriteriaEdit');
    };

    $scope.nextbtnRequiredDocument = function () {
        $state.go('AdmRequiredDocumentsProgramEdit');
    };

    $scope.nextbtnApplicationConfiguration = function () {
        $state.go('PreApplicationConfigurationEdit');
    };
    */

    /*-------------------MstProgrammePartGetByProgrammeIdAndProgInstId - Added By Kalpesh 13-04-2021 ----------------------------*/
    $scope.MstProgrammePartGetByProgrammeIdAndProgInstId = function () {
        if ($localStorage.PreProgInstData.ProgrammeId == null || $localStorage.PreProgInstData.ProgrammeId == undefined) {

            $scope.FindProgIdPre();
            // ProgInst.ProgrammeInstanceId
        }
        else {
            $scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
        }
        
 
        $http({
            method: 'POST',
            //url: 'api/ProgrammeInstancePart/MstProgrammePartGetByProgrammeIdAndProgInstId',
            url: 'api/ProgrammeInstancePart/GetMstProgrammePartByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                    $scope.ProgPartList = response.obj;
                    if ($localStorage.localObj.ProgrammePartId != null) {
                        $scope.getProgPartTermListByPartId();
                    }
                    $scope.ProgInst.ProgrammePartId = $localStorage.localObj.ProgrammePartId;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    

    /*Branch/Specialisation List By Programme Instance Id Bhavita - 13-04-21 */
    $scope.getBranchListByProgInstId = function () {
        
        $scope.BranchList = {};
        $http({
            method: 'POST',
            //url: 'api/ProgrammeInstancePart/MstProgrammeBranchListGetByProgInstanceId',
            url: 'api/ProgrammeInstancePart/GetMstSpecialisationByProgId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.BranchList = response.obj;
                //$scope.TestCountry = {​​​​​
                //}​​​​​;
            })
            .error(function (res) {
                //alert(res);
            });
    };

    /*Programme Part Term List By Programme Instance Id Bhavita - 13-04-21 */
    $scope.getProgPartTermListByProgInstPartId = function () {
 
        $http({
            method: 'POST',
            //url: 'api/ProgrammeInstancePart/ProgrammePartTermGetByProgInstId',
            url: 'api/ProgrammeInstancePart/ProgrammePTGetByProgIdandBranchId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };


});

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}

