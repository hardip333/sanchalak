app.controller('AddOnInfoReportCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Add On Information Details";

    $scope.obj = {};
    $scope.Id = {};
    $scope.Institute = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.cancelAddOnInformationReport = function () {
        $scope.AddOnInfo = {};
        $scope.Isvisible = false;
        $scope.IsAddOnVisible = false;
        $scope.NoRecLabel = false;

    }
    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.Institute = response.obj[0];
                
            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.getAcademicList = function () {
        $scope.AcadList = {};
        $http({
            method: 'POST',
            url: 'api/AddOnInformation/AcademicYearGet',
            //data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getInstanceNameList = function () {
        $scope.InstanceNameList = {};
        $scope.AddOnInfo = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.AddOnInfo.AcademicYearId }

        $http({
            method: 'POST',
            url: 'api/AddOnInformation/IncProgramInstancePartTermGetbyFacId',
            data: $scope.AddOnInfo,
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

                    $scope.InstanceNameList = response.obj;  
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getAddOnInformationReport = function () {
        $scope.IsAddOnVisible = true;
        $scope.obj = {};
        $scope.obj.InstituteId = $scope.Institute.InstituteId;
        $scope.obj.IncProgInstId = $scope.AddOnInfo.ProgrammeInstance.Id;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/AddOnInformation/AddOnInformationReport',
            data: $scope.obj,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                }

                else {
                    $scope.offSpinner();
                    $scope.Isvisible = true;                  
                    $scope.addOnInfo = response.obj;
                   
                    $scope.AddOnInformationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })
                    $scope.getAdmStuentAddOnReport();
                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };

    $scope.getAdmStuentAddOnReport = function () {
     
        $scope.obj = {};      
        $scope.obj.IncProgInstId = $scope.AddOnInfo.ProgrammeInstance.Id;
     
        $http({
            method: 'POST',
            url: 'api/AddOnInformation/AdmStudentAddOnReport',
            data: $scope.obj,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.obj == "No Record Found") {
                       
                        $scope.NoRecLabel = true;
                        $scope.IsAddOnVisible = false;
                        $scope.IsExcelButton = false;
                      
                      

                    }
                    
                   
                }
                                               
                    else {

                    
                        $scope.NoRecLabel = false;
                        $scope.IsAddOnVisible = true;   
                        $scope.IsExcelButton = true;


                        $scope.admStudentAddOnTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })
                        $scope.getAddOnInfoStudentExcel();
                    }
                       
                    
                   
                

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };

    
    $scope.displayStudent = function (data) {
      
        $scope.Studentinfo = data;

        $http({
            method: 'POST',
            url: 'api/AddOnInformation/AdmStudentTitleAddOnvalueReport',
            data: $scope.Studentinfo,
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
                   
                        
                    $scope.addOnTitle = response.obj;
                   
                   
                        $scope.AddOnTitleTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })
                   
                        

                    
                  
                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };

    $scope.getAddOnInfoStudentExcel = function () {
       
        $scope.obj = {};
        $scope.obj.IncProgInstId = $scope.AddOnInfo.ProgrammeInstance.Id;

        $http({
            method: 'POST',
            url: 'api/AddOnInformation/AddonInfoReportExcel',
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

                   
                    $scope.addOnInfoReportExcel = response.obj;
                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    };


    $scope.exportDataAddOnInfo = function () {
       
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "AddOnInformationDetails" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'AddOnInformationDetails| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationFormNumber', title: 'Application No' },
                { columnid: 'PRN', title: 'Applicant UserName' },
                { columnid: 'NameAsPerMarkSheet', title: 'Applicant Name' },
                { columnid: 'MobileNo', title: 'MobileNo' },
                { columnid: 'EmailId', title: 'EmailId' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },             
                { columnid: 'TitleName', title: 'Title Name' },
                { columnid: 'AddOnValue', title: 'Value Name' },
              




            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.addOnInfoReportExcel]);
    };
});