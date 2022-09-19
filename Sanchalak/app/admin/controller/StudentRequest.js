app.controller('StudentRequestCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];
                // $scope.Faculty = response.obj; // Krunal's code               
                $scope.getProgrammeList();
                         

            })
            .error(function (res) {
                alert(res);
            });
    };  

    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammeList = function () {
       
        $scope.ProgList = {};
        
        var InstituteId = { InstituteId: $scope.Institute.InstituteId }
        $http({
            method: 'POST',
            url: 'api/StudentRequest/MstProgrammeListGet',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    $scope.ProgList = {};

                }
                else {
                    $scope.ProgList = response.obj;
                    
                }
                
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgrammeListByFacId = function () {

        if ($scope.StudentRequest.FacultyId != null && $scope.StudentRequest.FacultyId != undefined) {
            var data = { FacultyId: $scope.StudentRequest.FacultyId };
        } else {
            alert("Something Went Wrong!!!");
        }

        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammeGetByFacId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgList = {};
                }
                else {
                    $scope.ProgList = response.obj;
                    
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.submit = function () {
        
        if ($scope.StudentRequest.ProgrammeId === null || $scope.StudentRequest.ProgrammeId === undefined || $scope.StudentRequest.ProgrammeId === "") {
            alert("Please Select Programme Name");
        }
        else {

            $scope.onSpinner();
            var data = { ProgrammeId: $scope.StudentRequest.ProgrammeId };
            $http({
                method: 'POST',
                url: 'api/StudentRequest/StudentRequestGetByProgrammeId',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');
                        $scope.offSpinner();

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.offSpinner();
                    }
                    else {

                        $scope.StudentRequestListTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj

                        });
                        $scope.offSpinner();

                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.displayStudentRequestData = function (data) {
        $scope.newStudentRequest = data;
    };

    $scope.RequestApprove = function (data) {
        
        $scope.StudentData = data;

        $http({
            method: 'POST',
            url: 'api/StudentRequest/StudentRequestHandleApprove',
            data: $scope.StudentData,
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
                    $scope.submit();
                    $('#large-Modal').modal('hide');
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.RequestReject = function (data) {
        if ($scope.newStudentRequest.RemarksByFaculty === null || $scope.newStudentRequest.RemarksByFaculty === undefined || $scope.newStudentRequest.RemarksByFaculty === "") {
            alert("Please Enter Remarks");
        }
        else {
            $scope.StudentData = data;

            $http({
                method: 'POST',
                url: 'api/StudentRequest/StudentRequestHandleReject',
                data: $scope.StudentData,
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
                        $scope.submit();
                        $('#large-Modal').modal('hide');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };

    $scope.RequestRejectByAcad = function (data) {
        if ($scope.newStudentRequest.RemarksByAcademic === null || $scope.newStudentRequest.RemarksByAcademic === undefined || $scope.newStudentRequest.RemarksByAcademic === "") {
            alert("Please Enter Remarks");
        }
        else {
            $scope.StudentData = data;

            $http({
                method: 'POST',
                url: 'api/StudentRequest/StudentRequestHandleReject',
                data: $scope.StudentData,
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
                        $scope.submit();
                        $('#large-Modal').modal('hide');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };
 
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


});