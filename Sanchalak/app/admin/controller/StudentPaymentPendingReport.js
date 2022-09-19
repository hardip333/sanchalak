app.controller('StudentPaymentPendingReportCtrl', function ($scope, $http,$filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $localStorage) {

    $rootScope.pageTitle = "Student Payment Pending Report";

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    /*Back to Edit Page of Subject*/
    $scope.backToList = function () {
        $scope.StudentPaymentPendingList = {};
        $scope.filter = {};
        $scope.ShowSPPTable = false;
        $scope.NoRecLabel = false;
        $scope.IsExcelButton = false;
        $scope.Backbtn = false;
        $scope.showFormFlag = false;
        $scope.ShowSPPAllProgTable = false;
        $scope.showFormFlagAllProg = false;
    }; 
    $scope.getProgInstanceList = function () {

            $http({
                method: 'POST',
                url: 'api/StudentPaymentPendingReport/ProgrammeInstanceDD',
                data: { AcademicYearId: $scope.filter.AcademicYearId, FacultyId: $scope.filter.FacultyId },
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
                           
                            $scope.tempList = response.obj;
                            $scope.ProgrammeListGetForDropDownList = [];
                            $scope.defaultProgList = [];
                            var programmeObj = {
                                Id: 0,
                                InstanceName: "All Programme"
                            }
                            $scope.defaultProgList.push(programmeObj);             
                            $scope.ProgrammeListGetForDropDownList = $scope.defaultProgList.concat($scope.tempList);
                           
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        
    };
    $scope.getStudentPaymentPendingList = function () {
       $scope.StudentPayment = {};
       
        $scope.StudentPayment.AcademicYearId = $scope.filter.AcademicYearId;
        $scope.StudentPayment.FacultyId = $scope.filter.FacultyId;
        $scope.StudentPayment.ProgrammeInstanceId = $scope.filter.ProgrammeInstanceId;
        $scope.StudentPayment.BranchId = $scope.filter.SpecialisationId;
        $scope.StudentPayment.ProgrammeInstancePartId = $scope.filter.ProgrammeInstancePartId;
        $scope.StudentPayment.ProgrammeInstancePartTermId = $scope.filter.SourcePartTermId;
       
        

        $scope.StudentPaymentPendingList = {};
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/StudentPaymentPendingReport/StudentPaymentPendingReportGeyById',
            data: $scope.StudentPayment,
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
                        $scope.offSpinner();
                        $scope.StudentPaymentPendingList = {};
                        //$scope.filter = {};
                        $scope.ShowSPPTable = false;
                        $scope.ShowSPPAllProgTable = false;
                        alert(response.obj);
                        $scope.NoRecLabel = true;
                        $scope.IsExcelButton = false;
                        $scope.Backbtn = false;
                        $scope.showFormFlag = false;
                        $scope.showFormFlagAllProg = false;
                       
                    }
                }
                else {
                    
                    $scope.offSpinner();                
                    $scope.Backbtn = true;
                    $scope.ShowSPPTable = true;
                    $scope.ShowSPPAllProgTable = false;
                    $scope.showFormFlag = true;
                    $scope.showFormFlagAllProg = false;
                    $scope.NoRecLabel = false;
                    $scope.IsExcelButton = true;
                  
                    $scope.StudentPaymentPendingList = response.obj;

                    for (let i = 0; i < $scope.StudentPaymentPendingList.length; i++) {
                        $scope.StudentPayment.FacultyName = $scope.StudentPaymentPendingList[i].FacultyName;
                        $scope.StudentPayment.AcademicYearCode = $scope.StudentPaymentPendingList[i].AcademicYearCode;
                        $scope.StudentPayment.BranchName = $scope.StudentPaymentPendingList[i].BranchName;
                        $scope.StudentPayment.ProgrammeInstanceName = $scope.StudentPaymentPendingList[i].ProgrammeInstanceName;
                        $scope.StudentPayment.ProgrammeInstancePartName = $scope.StudentPaymentPendingList[i].ProgrammeInstancePartName; 
                        $scope.StudentPayment.InstancePartTermName = $scope.StudentPaymentPendingList[i].InstancePartTermName;
                        $scope.StudentPayment.count = $scope.StudentPaymentPendingList[i].IndexId;
                        
                    }
                 
                    $scope.StudentPaymentPendingTableParams = new NgTableParams({
                    }, {
                            dataset: response.obj,
                            

                    });

                  
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getStudentPaymentPendingListAllProgramme = function () {
        $scope.StudentPayment = {};

        $scope.StudentPayment.AcademicYearId = $scope.filter.AcademicYearId;
        $scope.StudentPayment.FacultyId = $scope.filter.FacultyId;
        $scope.StudentPayment.ProgrammeInstanceId = $scope.filter.ProgrammeInstanceId;
        $scope.StudentPayment.BranchId = $scope.filter.SpecialisationId;
        $scope.StudentPayment.ProgrammeInstancePartId = $scope.filter.ProgrammeInstancePartId;
        $scope.StudentPayment.ProgrammeInstancePartTermId = $scope.filter.SourcePartTermId;



        $scope.StudentPaymentPendingList = {};
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/StudentPaymentPendingReport/StudentPaymentPendingReportGeyById',
            data: $scope.StudentPayment,
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
                        $scope.offSpinner();
                        $scope.StudentPaymentPendingList = {};
                        //$scope.filter = {};
                        $scope.ShowSPPTable = false;
                        $scope.ShowSPPAllProgTable = false;
                        alert(response.obj);
                        $scope.NoRecLabel = true;
                        $scope.IsExcelButton = false;
                        $scope.Backbtn = false;
                        $scope.showFormFlag = false;
                        $scope.showFormFlagAllProg = false;

                    }
                }
                else {

                    $scope.offSpinner();
                    $scope.Backbtn = true;
                    $scope.ShowSPPTable = false;
                    $scope.ShowSPPAllProgTable = true;
                    $scope.showFormFlag = false;
                    $scope.showFormFlagAllProg = true;
                    $scope.NoRecLabel = false;
                    $scope.IsExcelButton = true;

                    $scope.StudentPaymentPendingList = response.obj;
                    for (let i = 0; i < $scope.StudentPaymentPendingList.length; i++) {
                        $scope.StudentPayment.FacultyName = $scope.StudentPaymentPendingList[i].FacultyName;
                        $scope.StudentPayment.AcademicYearCode = $scope.StudentPaymentPendingList[i].AcademicYearCode;
                        $scope.StudentPayment.count = $scope.StudentPaymentPendingList[i].IndexId;
                       
                    }
                   

                    $scope.StudentPaymentPendingAllProgTableParams = new NgTableParams({
                    }, {
                            dataset: response.obj,
                           

                    });


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getAllProgrammeDetails = function (data) {
       
        if (data.ProgrammeInstanceId == 0) {
            $scope.IsVisibleNotAllProgramme = false;
            $scope.IsVisBtnAllProg = true;
            $scope.IsVisBtnNotAllProg = false;
            $scope.ShowSPPTable = false;
            $scope.ShowSPPAllProgTable = false;
            $scope.NoRecLabel = false;
            $scope.IsExcelButton = false;
            $scope.Backbtn = false;
            $scope.showFormFlag = false;
            $scope.showFormFlagAllProg = false;
        }
        else {
            $scope.IsVisibleNotAllProgramme = true;
            $scope.IsVisBtnNotAllProg = true;
            $scope.IsVisBtnAllProg = false;
            $scope.ShowSPPTable = false;
            $scope.ShowSPPAllProgTable = false;
            $scope.NoRecLabel = false;
            $scope.IsExcelButton = false;
            $scope.Backbtn = false;
            $scope.showFormFlag = false;
            $scope.showFormFlagAllProg = false;
            $scope.getBranchList(data);
        }
      
       
    }

    $scope.exportDataStudentPaymentPendingReport = function () {
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentPaymentPendingReport" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'Student Payment Pending Report |Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AcademicYearCode', title: 'Academic Year Code' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeInstanceName', title: 'Programme Instance Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'ProgrammeInstancePartName', title: 'Programme Instance Part Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term Name' },
                { columnid: 'StudentPRN', title: 'Student PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Student Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'TotalAmount', title: 'Total Amount' },
                { columnid: 'AmountPaid', title: 'Amount Paid' },
                { columnid: 'IsInstalmentSelected', title: 'Is Instalment Selected' },
                { columnid: 'TotalInstalmentGiven', title: 'Total Instalment Given' },
                { columnid: 'TotalInstalmentSelected', title: 'Total Instalment Selected' },
                { columnid: 'InstalmentNo', title: 'No Of Instalment Paid' },
                { columnid: 'RemainingInstallment', title: 'Remaining Installment ' },

               







            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudentPaymentPendingList]);
    };

   




   



    


});