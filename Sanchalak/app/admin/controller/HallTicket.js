app.controller('HallTicketCtrl', function ($scope, $http,$filter, $rootScope, Upload, $localStorage, $state, $cookies, $location, $mdDialog, NgTableParams, $timeout) {

    $scope.HallTicket = {};    
    
    $scope.getExamEvent = function () {

        $http({
            method: 'POST',
            url: 'api/HallTicket/ExamEventGetForDropDown',            
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamMasterListGetActiveList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getInstituteByExamEvent = function () {
        $scope.InstituteList = {};
        data = { ExamMasterId: $scope.HallTicket.ExamMasterId };

        $http({
            method: 'POST',
            url: 'api/HallTicket/MstInstituteGetByExamEvent',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
            });
    };    

    $scope.getProgrammeByFacultyExamId = function () {
        $scope.ProgrammeList = {};
        data = { FacultyExamMapId: $scope.HallTicket.FacultyExamId };

        $http({
            method: 'POST',
            url: 'api/HallTicket/MstProgrammeGetByFacultyExamId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getBranchByProgrammeId = function () {
        $scope.BranchList = {};
        data = { ProgrammeId: $scope.HallTicket.ProgrammeId };

        $http({
            method: 'POST',
            url: 'api/HallTicket/MstProgrammeBranchListGetByProgrammeId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammePartTermByProgrammeId = function () {
        $scope.ProgrammePartTermList = {};
        data = { ProgrammeId: $scope.HallTicket.ProgrammeId };

        $http({
            method: 'POST',
            url: 'api/HallTicket/MstProgrammePartTermGetbyProgrammeId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammePartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.checkRadio = function (data) {
        
        $scope.HallTicket.HTRadio = data;

        if ($scope.HallTicket.HTRadio == 'PRNWise') {
            $scope.PRN = true;
        }
        else {
            $scope.PRN = false;
        }
    };

    $scope.getProgrammeInstancePartTermByPartTermBranch = function () {
        $scope.ProgrammeInstancePartTermList = {};
        data = {
            ProgrammePartTermId: $scope.HallTicket.ProgrammePartTermId,
            SpecialisationId: $scope.HallTicket.SpecialisationId
        };
        

        $http({
            method: 'POST',
            url: 'api/HallTicket/IncProgrammeInstancePartTermGetByPartTermAndBranch',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeInstancePartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.submit = function () {
        
        if ($scope.HallTicket.HTRadio == 'PRNWise') {
            if ($scope.HallTicket.ExamMasterId == null || $scope.HallTicket.ExamMasterId == undefined || $scope.HallTicket.ExamMasterId == "" ||
                $scope.HallTicket.FacultyExamId == null || $scope.HallTicket.FacultyExamId == undefined || $scope.HallTicket.FacultyExamId == "" ||
                $scope.HallTicket.ProgrammeId == null || $scope.HallTicket.ProgrammeId == undefined || $scope.HallTicket.ProgrammeId == "" ||
                $scope.HallTicket.SpecialisationId == null || $scope.HallTicket.SpecialisationId == undefined || $scope.HallTicket.SpecialisationId == "" ||
                $scope.HallTicket.ProgrammePartTermId == null || $scope.HallTicket.ProgrammePartTermId == undefined || $scope.HallTicket.ProgrammePartTermId == "") {

                alert("Please Select All Fields Before Click Submit !!!");
                
            }

            else {

                var data = {
                    ExamMasterId: $scope.HallTicket.ExamMasterId,
                    BranchId: $scope.HallTicket.SpecialisationId,
                    ProgrammeInstancePartTermId: $scope.HallTicket.ProgrammeInstancePartTermId,
                    PRN: $scope.HallTicket.PRN
                };

                $http({
                    method: 'POST',
                    url: 'api/ExamFormMaster/generateSingleHallTicket',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                            $scope.offSpinner();
                        }
                        else {
                            alert(response.obj);
                            $scope.offSpinner();
                            window.open(response.obj, '_blank');
                            
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        }

        if ($scope.HallTicket.HTRadio == 'Bulk') {
            if ($scope.HallTicket.ExamMasterId == null || $scope.HallTicket.ExamMasterId == undefined || $scope.HallTicket.ExamMasterId == "" ||
                $scope.HallTicket.FacultyExamId == null || $scope.HallTicket.FacultyExamId == undefined || $scope.HallTicket.FacultyExamId == "" ||
                $scope.HallTicket.ProgrammeId == null || $scope.HallTicket.ProgrammeId == undefined || $scope.HallTicket.ProgrammeId == "" ||
                $scope.HallTicket.SpecialisationId == null || $scope.HallTicket.SpecialisationId == undefined || $scope.HallTicket.SpecialisationId == "" ||
                $scope.HallTicket.ProgrammePartTermId == null || $scope.HallTicket.ProgrammePartTermId == undefined || $scope.HallTicket.ProgrammePartTermId == "") {

                alert("Please Select All Fields Before Click Submit !!!");

            }

            else {

                var data = {
                    ExamMasterId: $scope.HallTicket.ExamMasterId,
                    SpecialisationId: $scope.HallTicket.SpecialisationId,
                    ProgrammePartTermId: $scope.HallTicket.ProgrammePartTermId
                };

                $http({
                    method: 'POST',
                    url: 'api/ExamFormMaster/generateBulkHallTicket',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                            $scope.offSpinner();
                        }
                        else {
                            alert(response.obj);
                            $scope.offSpinner();
                            window.open(response.obj, '_blank');
                            
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        }

        if ($scope.HallTicket.HTRadio == 'BulkInSinglePage') {
            if ($scope.HallTicket.ExamMasterId == null || $scope.HallTicket.ExamMasterId == undefined || $scope.HallTicket.ExamMasterId == "" ||
                $scope.HallTicket.FacultyExamId == null || $scope.HallTicket.FacultyExamId == undefined || $scope.HallTicket.FacultyExamId == "" ||
                $scope.HallTicket.ProgrammeId == null || $scope.HallTicket.ProgrammeId == undefined || $scope.HallTicket.ProgrammeId == "" ||
                $scope.HallTicket.SpecialisationId == null || $scope.HallTicket.SpecialisationId == undefined || $scope.HallTicket.SpecialisationId == "" ||
                $scope.HallTicket.ProgrammePartTermId == null || $scope.HallTicket.ProgrammePartTermId == undefined || $scope.HallTicket.ProgrammePartTermId == "") {

                alert("Please Select All Fields Before Click Submit !!!");

            }

            else {

                var data = {
                    ExamMasterId: $scope.HallTicket.ExamMasterId,
                    SpecialisationId: $scope.HallTicket.SpecialisationId,
                    ProgrammePartTermId: $scope.HallTicket.ProgrammePartTermId
                };

                $http({
                    method: 'POST',
                    url: 'api/ExamFormMaster/generateBulkHallTicketInSinglePdf',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                            $scope.offSpinner();
                        }
                        else {
                            alert(response.obj);
                            $scope.offSpinner();
                            window.open(response.obj, '_blank');

                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        }

        
    };

    $scope.cancelSelection = function () {
        $scope.HallTicket = {};       
    };


    
    
});