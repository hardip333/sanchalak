app.controller('InstituteMapToPartTermCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Group-Preference-Instance Part Term Mapping";
    

    $scope.ProgInst = {};
  
    $scope.resetMapToPartTerm = function () {
        $scope.ProgInst = {};
        $scope.getFacultyById();
    };

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
                $scope.getProgrammeList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/AcademicYearGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                if ($localStorage.PreProgInstData.AcademicYearId != null) {

                    
                }
             
             

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammeList = function () {
        $scope.ProgInst.InstituteId = $scope.Institute.InstituteId;
        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/ProgrammeListGetByFacultyId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                  
                }
                else {
                   
                   
                    $scope.ProgrammeNameList = response.obj; 
                    $scope.getBranchListByProgrammeId(); 
                   
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getBranchListByProgrammeId = function () {
        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/MstProgrammeBranchListGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BranchList = {};
                }
                else {
                    $scope.BranchList = response.obj;
                    $scope.getProgrammePartListByProgrammeId();

                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammePartListByProgrammeId = function () {

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/ProgrammePartGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                    $scope.ProgPartList = response.obj;
                    $scope.getProgPartTermListByPartId(); 
                    
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgPartTermListByPartId = function () {

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/IncProgrammeInstancePartTermGetByPartId',
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

    $scope.getIncProgPartTermListByProgrammeId = function () {

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/IncProgrammeInstancePartTermGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartTermList = {};
                }
                else {
                    $scope.IncProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.MstGroupCodeGet = function () {
       
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/MstGroupCodeGet',
            data: data,
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
                    $scope.GroupCodeList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.MstPreferenceGroupGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/MstPreferenceGroupGet',
            data: data,
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
                    $scope.PreferenceGroupList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
   
    $scope.MstPreferenceCodeMapAdd = function () {
       
        if ($scope.ProgInst.CodeId === null || $scope.ProgInst.CodeId === undefined ||
            $scope.ProgInst.PreferenceId === null || $scope.ProgInst.PreferenceId === undefined ||
            $scope.ProgInst.DestinationIncProgInstPartTermId === null || $scope.ProgInst.DestinationIncProgInstPartTermId === undefined)
        {
            alert("please check all fields !!!");
        }
        else {

        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/MstPreferenceCodeMapAdd',
            data: $scope.ProgInst,
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
                    alert(response.obj);
                    $scope.MstPreferenceCodeMap = {};
                    $scope.MstPreferenceCodeMapGetByIns();

                    $scope.ProgInst = {};
                    $scope.IsVisible = false;
                    $scope.IsVisibleHeader = false;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        }
    }      
    
    $scope.MstPreferenceCodeMapGetByIns = function () {

        var data = new Object();
        $http({
            method: 'POST',
            url: 'api/InstituteMapToPartTerm/MstPreferenceCodeMapGet',
            data: data,
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

                    if (response.obj === "No Record Found") {
                        console.log(response.obj);
                        $scope.NoRecLabel = true;
                        console.log($scope.NoRecLabel);
                    }
                    else {




                        $scope.InstituteMap = {};

                        $scope.InstituteMap = response.obj;
                        console.log($scope.InstituteMap);
                        $scope.InstituteMapTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })
                    }
                 
                    if ($.fn.DataTable.isDataTable('#example')) {
                        $('#example').DataTable().destroy();
                        $('#example tbody').empty();
                    }
                    angular.element(document).ready(function () {

                        var today = new Date();
                        var dd = String(today.getDate()).padStart(2, '0');
                        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                        var yyyy = today.getFullYear();
                        today = mm + '/' + dd + '/' + yyyy;
                        console.log("Inside-Table");
                        var t = $('#example').DataTable({
                            data: response.obj,
                            "scrollX": true,
                            "pageLength": 7,
                            "columns": [
                                {
                                    data: 'Sr.No',
                                    render: function (data, type, row, meta) {
                                        return meta.row + 1;
                                    }
                                },

                                { "data": "GroupCode" },
                                { "data": "GroupName" },
                                { "data": "InstancePartTermName" },
                                { "data": "DestinationPartTermName" },
                               

                            ],
                            dom: 'Bfrtip',

                            buttons: [
                                'copy', 'csv',
                                { extend: 'excel', title: 'Report of Group-Preference-Instance Part Term Mapping AY 2020-21 (MSUIS)' + '-' + $scope.Institute.InstituteName + '-' + today, exportOptions: { columns: "thead th:not(.noExport)" } }

                                , 'pdf', { extend: 'print', message: '<h3> This is an MSU-IS Report<br> Done <h3>' }

                            ]
                        });


                        t.on('order.dt search.dt', function () {
                            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = i + 1;
                            });
                        }).draw();


                    });
                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };
  
    $scope.ShowPreferenceDetails = function () {

        if (
            $scope.ProgInst.AcademicYearId === null || $scope.ProgInst.AcademicYearId === undefined ||
            $scope.ProgInst.ProgrammeId === null || $scope.ProgInst.ProgrammeId === undefined ||
            $scope.ProgInst.SpecialisationId === null || $scope.ProgInst.SpecialisationId === undefined ||
            $scope.ProgInst.ProgrammePartId === null || $scope.ProgInst.ProgrammePartId === undefined ||
            $scope.ProgInst.ProgInstPartTermId === null || $scope.ProgInst.ProgInstPartTermId === undefined 
           
        ) {

            alert("please select all fields !!!");
        }
        else {
            $scope.IsVisibleHeader = true;
            $scope.IsVisible = true;
            $scope.MstGroupCodeGet();
            $scope.MstPreferenceGroupGet();
            $scope.getIncProgPartTermListByProgrammeId(); 
        }
       
    }


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

