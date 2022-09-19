app.controller('ApplicationInstitutePreferenceReportCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage GradeScale";
    $scope.obj = {};
    $scope.InstitutePreference1 = {};
    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
              
                $scope.Institute = response.obj[0];
                console.log($scope.Institute);
                $scope.getInstanceNameList();
            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.IncAcademicYearListGet = function () {

        $http({
            method: 'POST',
            url: 'api/ApplicationPreferenceReport/IncAcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AcademicYearList = response.obj;
                
              

            })
            .error(function (res) {

            });
    };
    $scope.Id = {};
    $scope.Institute = {};
    $scope.getInstanceNameList = function () {
       
        $scope.obj = {};
        $scope.obj.InstituteId = $scope.Institute.InstituteId;
        //$scope.obj.AcademicYearId = $scope.AcademicYearList.Id;
       
        $http({
            method: 'POST',
            url: 'api/ApplicationPreferenceReport/IncProgramInstancePartTermGetbyFacId',
            data: $scope.obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                   
                }
                else {
                                     
                    $scope.InstanceNameList = response.obj;
                    //$scope.getApplicationInstitutePreferenceReport();
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };
  
   $scope.getApplicationInstitutePreferenceReport = function () {
      
        $scope.obj = {};
        $scope.obj.InstituteId = $scope.Institute.InstituteId;
        $scope.obj.IncProgInstId = $scope.InstitutePreference.ProgrammeInstance.Id;
        //$("#example").reload();
        $http({
            method: 'POST',
            url: 'api/ApplicationPreferenceReport/ApplicationInstitutePreferenceReport',
            data: $scope.obj,
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
                   // $("#example").dataTable().fnDestroy();
                  
                    $scope.InstitutePreference1 = {};

                    $scope.InstitutePreference1 = response.obj;
                    $scope.InstPreferenceTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })
                    //$('#example').DataTable().ajax.reload(true, true);
                    
                   
                    //$("#example").dataTable().fnDestroy();
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
                           
                                { "data": "ApplicationNo" },
                                { "data": "InstituteName" },
                                { "data": "ProgrammeName" },
                                { "data": "BranchName" },
                                { "data": "InstancePartTermName" },
                                { "data": "PreferenceNo" }, 
                                { "data": "PreferenceGivenOn" }
                               
                            ],
                            dom: 'Bfrtip',
                      
                            buttons: [
                                'copy', 'csv',
                                { extend: 'excel', title: 'Report of Institute Preference AY 2020-21 (MSUIS)'+'-'+ $scope.Institute.InstituteName +'-' + today, exportOptions: { columns: "thead th:not(.noExport)" } }

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
   
    

  

});