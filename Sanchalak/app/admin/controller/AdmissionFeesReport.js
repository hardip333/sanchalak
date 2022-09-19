app.controller('AdmissionFeesReportCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Application Fees Report";
    $scope.ADFP = {};

    $scope.AcademicYearGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
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
            url: 'api/FeeConfiguration/FacultyGet',
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
            url: 'api/FeeConfiguration/InstanceListGetbyFacultyIdAndAcadId',
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
                    $scope.AdmissionFeesList = {};
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
            url: 'api/ProgrammeInstancePart/MstProgrammePartGetByProgrammeIdAndProgInstId',
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
                    $scope.AdmissionFeesList = {};
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getBranchListByProgInstId = function () {
        $scope.BranchList = {};

        $scope.ADFP.Id = $scope.ADFP.ProgrammeInstanceId;
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/MstProgrammeBranchListGetByProgInstanceId',
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
                        $scope.BranchList = {};

                    }
                }
                else {
                    $scope.AdmissionFeesList = {};
                    $scope.BranchList = response.obj;
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
            url: 'api/ProgrammeInstancePart/ProgrammePartTermGetByProgInstId',
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
                    $scope.AdmissionFeesList = {};
                    $scope.ProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }



    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    
    $scope.AdmissionFeesReport = function () {

        var data = new Object();
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AdmissionFeesReport',
            data: $scope.ADFP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    /*$rootScope.$broadcast('dialog', "Error", "alert", response.obj);*/
                    alert(response.obj);
                    $scope.offSpinner();
                }
                else {
                    
                    $scope.offSpinner();
                    $scope.AdmissionFeesList = response.obj;
                    console.log($scope.AdmissionFeesList);
                    
                    
                    
                    $scope.Tot = 0;
                    var AdmissionFeesList = new Array();
                    for (var j in $scope.AdmissionFeesList) {
                        $scope.Tot = parseFloat($scope.AdmissionFeesList[j].AmountPaid) + parseFloat($scope.Tot);

                        
                        if ($scope.AdmissionFeesList[j].IsInstalmentSelectedSts == "NULL") {
                            $scope.AdmissionFeesList[j].IsInstalmentSelectedSts = "-";
                        }
                        else if ($scope.AdmissionFeesList[j].IsInstalmentSelectedSts == "True") {
                            $scope.AdmissionFeesList[j].IsInstalmentSelectedSts = "YES";
                        }
                        else 
                        {
                            $scope.AdmissionFeesList[j].IsInstalmentSelectedSts = "NO";
                        }
                        if ($scope.AdmissionFeesList[j].OrderId == "Not Defined") {
                            $scope.AdmissionFeesList[j].TotalInstalmentGiven = "-";
                            $scope.AdmissionFeesList[j].TotalInstalmentSelected = "-";
                            $scope.AdmissionFeesList[j].RemainingInstallment = "-";
                            $scope.AdmissionFeesList[j].InstalmentNo = "-";
                            $scope.AdmissionFeesList[j].OrderId = "-";
                            $scope.AdmissionFeesList[j].AmountPaid="-"
                        }
                        
                        
                        
                        
                    }
                    

                    if ($.fn.dataTable.isDataTable('#example')) {
                        $('#example').dataTable().fnClearTable();
                        $('#example').DataTable().destroy();

                    }
                    $(document).ready(function () {
                        $('#example').DataTable({
                            "ordering": false,
                            dom: 'Bfrtip',
                            buttons: [
                                //{
                                //    extend: 'csv', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.FTR.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" }

                                //}
                                //,
                                { extend: 'excel', title: $scope.AdmissionFeesList[0].InstancePartTermName + ' ' + 'Admission Fee Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                /*, { extend: 'pdf', orientation: 'landscape', title: $scope.AdmissionFeesList[0].InstancePartTermName + '\n' + 'Admission Fee Report', exportOptions: { columns: "thead th:not(.noExport)" } }*/
                                /* , { extend: 'print', title: 'Fee Publish Report' + $scope.PublishList[0].AcademicYearCode + ':: ' + $scope.PublishList[0].FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }*/

                            ]
                        });
                    });
                }
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    

});