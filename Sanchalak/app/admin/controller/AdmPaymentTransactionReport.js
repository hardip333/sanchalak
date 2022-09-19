app.controller('AdmPaymentTransactionCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Payment Transaction Report";

    $scope.cardTitle = "Payment Transaction Report Operation";
    $scope.PayTrans = {};
    

    $scope.resetPaymentTransaction = function () {
        $scope.PayTrans = {};
        $localStorage.AdmissionFeeReport = {};
    };

    $scope.setValue = function () {
        if ($localStorage.AdmissionFeeReport.FlagFromProfile == true) {

            $scope.PayTrans.InstituteId = $localStorage.AdmissionFeeReport.InstituteId;          
            $scope.PayTrans.ProgrammeInstancePartTermId = $localStorage.AdmissionFeeReport.ProgrammeInstancePartTermId;
            $scope.PayTrans.Status = $localStorage.AdmissionFeeReport.TransactionStatus;
            $scope.PayTrans.FromDate = $localStorage.AdmissionFeeReport.FromDate;
            $scope.PayTrans.ToDate = $localStorage.AdmissionFeeReport.ToDate;
            $scope.getProgInsPartTermByInstituteId();
            $scope.getAdmPaymentTransByPartTermId();
            
            $localStorage.AdmissionFeeReport.FlagFromProfile = false;
        }
        else {
            $localStorage.AdmissionFeeReport = {};
        }
    };

    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {        
                $scope.Institute = response.obj[0];
                $scope.PayTrans.InstituteId = $scope.Institute.InstituteId;              
                $scope.getProgInsPartTermByInstituteId();
                $localStorage.AdmissionFeeReport = {};
                $localStorage.AdmissionFeeReport.FacultyId = $scope.Institute.Id;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgInsPartTermByInstituteId = function () {
      
        $http({
            method: 'POST',
            url: 'api/AdmPaymentTransaction/AdmPayTransPartTermGetByInstituteId',
            data: { InstituteId: $scope.PayTrans.InstituteId },
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

                        $scope.ProgrammeInstancePartTermList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
           
    };

    $scope.getAdmPaymentTransByPartTermId = function () {
        if ($scope.PayTrans.Status == null || $scope.PayTrans.Status === undefined ||
            $scope.PayTrans.ProgrammeInstancePartTermId == null || $scope.PayTrans.ProgrammeInstancePartTermId === undefined ||
            $scope.PayTrans.FromDate == null || $scope.PayTrans.FromDate === undefined ||
            $scope.PayTrans.ToDate == null || $scope.PayTrans.ToDate === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#PaymentReport')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/AdmPaymentTransaction/AdmPaymentTransGetByPartTermId',
                data: $scope.PayTrans,
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
                            $scope.PaymentTransTableParams = new NgTableParams({},
                                { dataset: null });
                        }
                        else {

                            $scope.PaymentTransTableParams = new NgTableParams({},
                                { dataset: response.obj });
                            $scope.PaymentTransList = response.obj;

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.getAdmPaymentTransByOrderId = function () {
       
        $http({
            method: 'POST',
            url: 'api/AdmPaymentTransaction/AdmPaymentTransGetByOrderId',
            data: { OrderId: $localStorage.AdmissionFeeReport.OrderId},
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

                        $scope.StudentPaymentInfo = response.obj[0];                            
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.ViewPaymentInfoByOrderId = function (OrderId) {
       
        $localStorage.AdmissionFeeReport.OrderId = OrderId;
        $localStorage.AdmissionFeeReport.FromDate = $scope.PayTrans.FromDate;
        $localStorage.AdmissionFeeReport.ToDate = $scope.PayTrans.ToDate;
        $state.go('StudentPaymentTransactionInfo');
    };

    $scope.backToList = function () {
        $localStorage.AdmissionFeeReport.TransactionStatus = $scope.StudentPaymentInfo.TransactionStatus;
        $localStorage.AdmissionFeeReport.ProgrammeInstancePartTermId = $scope.StudentPaymentInfo.ProgrammeInstancePartTermId;
        $localStorage.AdmissionFeeReport.InstituteId = $scope.StudentPaymentInfo.InstituteId;
        $localStorage.AdmissionFeeReport.FlagFromProfile = true;       

        if ($localStorage.AdmissionFeeReport.FacultyId == undefined || $localStorage.AdmissionFeeReport.FacultyId == null) {
           
            $state.go('AdmPaymentTransactionReportByInstitute');
        }
        else {
            $state.go('AdmPaymentTransactionReport');
        }
      
      
    };

    $scope.getMstInstitute = function () {
      
        $http({
            method: 'POST',
            url: 'api/AdmPaymentTransaction/MstInstituteGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;
                $localStorage.AdmissionFeeReport = {};
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.exportAdmissionFeeReport = function () {

        if ($scope.PaymentTransList == undefined) {

            alert("Please Mendatory fields then click on Submit");
            return false;
        }
     
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "AdmissionFeeReport_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: '  Admission Fee Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term Name' },
                { columnid: 'OrderId', title: 'Order Id' },
                { columnid: 'Name', title: 'Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'TransactionStatus', title: 'Status' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'TransactionDate', title: 'Transaction Date' },
              
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PaymentTransList]);

    };

});