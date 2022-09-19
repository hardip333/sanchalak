app.controller('ExamFormMasterListGeneratedCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    //debugger
    $rootScope.pageTitle = "Manage Exam Form Master List Generated";

    $scope.cardTitle = "Datewise Centerwise Paper Report";

    $scope.ExamFormMasterListGenerated = {};

    //$localStorage.PaperList = {};

    $scope.ShowFlag = false;
    
    $scope.resetExamFormMasterListGenerated = function () {
        $scope.ExamFormMasterListGenerated = {};
    };

    $scope.getExamEventMasterList = function () {

        $http({
            method: 'POST',
            url: 'api/ExamFormMasterListGenerated/ExamEventGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ExamEventList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/DatewiseCenterwisePaperReport/FacultyGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.FacultyList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgGetByFacultyId = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/ExamFormMasterListGenerated/MstProgrammeGetByFacultyId',
            data: { FacultyId: $scope.ExamFormMasterListGenerated.FacultyId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ProgList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getSpecialisationGetByPId = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/ExamFormMasterListGenerated/MstSpecialisatinGetByProgrammmeId',
            data: { ProgrammeId: $scope.ExamFormMasterListGenerated.ProgrammeId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.SpecList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code  

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgPartTermSpecId = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/ExamFormMasterListGenerated/MstProgrammePartTermGetBySpeId',
            data: { SpecialisationId: $scope.ExamFormMasterListGenerated.SpecialisationId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ProgPartTermList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code  

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getExamFormMasterListGenerated = function (ProgrammePartTermId) {
        
        
        $http({
            method: 'POST',
            url: 'api/ExamFormMasterListGenerated/ExamFormMasterListGeneratedGet',
            data: {
                ExamMasterId: $scope.ExamFormMasterListGenerated.ExamMasterId,
                FacultyId: $scope.ExamFormMasterListGenerated.FacultyId,
                ProgrammeId: $scope.ExamFormMasterListGenerated.ProgrammeId,
                SpecialisationId: $scope.ExamFormMasterListGenerated.SpecialisationId,
                ProgrammePartTermId: $scope.ExamFormMasterListGenerated.ProgrammePartTermId},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.ExamFormMasterListGeneratedParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.ExamFormMasterListGeneratedTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.ExamFormMasterListGeneratedData = response.obj[0];
                        
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };




    $scope.getPaperListGetByExamFormMaster = function (ExamMasterId, PRN, ProgInstancePartTermId) {
        
        $http({
            method: 'POST',
            url: 'api/ExamFormMasterListGenerated/PaperListGetByExamFormMaster',
            data: {
                ExamMasterId: $scope.ExamFormMasterListGenerated.ExamMasterId,
                PRN: PRN,
                ProgInstancePartTermId: ProgInstancePartTermId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.PaperListGetByExamFormMasterTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.PaperListGetByExamFormMasterTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.PaperListData = response.obj[0];
                        $scope.PaperList = response.obj;
                       

                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    

    


   

   

});