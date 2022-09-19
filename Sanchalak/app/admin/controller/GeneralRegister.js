app.controller('GeneralRegisterCtrl', function ($scope, $http,$filter, $rootScope, Upload, $localStorage, $state, $cookies, $location, $mdDialog, NgTableParams, $timeout) {
        
    $scope.showTimeTable = false;
    $scope.ShowLabel = false;   
    
    //Spinner ON
    $scope.onSpinner = function on() {

        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    

    $scope.submit = function () {       
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/GeneralRegister/GeneralRegisterReport',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                    $scope.offSpinner();

                } else if (response.response_code != "200") {
                    //$rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
                    $scope.offSpinner();
                }
                else {

                    if (response.obj == "No Record Found") {
                        $scope.offSpinner();
                        $scope.ShowLabel = true;
                        $scope.showTimeTable = false;
                    } else {
                        $scope.offSpinner();
                        $scope.showTimeTable = true;
                        $scope.ShowLabel = false;
                        $scope.GeneralRegistertableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                        $scope.GeneralRegister = response.obj;                            
                    }
                        
                }
            })
            .error(function (res) {
                //$rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                alert(res.obj);
                $scope.offSpinner();
            });
    
    };
   
    $scope.exportData = function () {
        
        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "GeneralRegisterReport_" + DateWithoutDashed + time;
        
        var mystyle = {
            headers: true,
            //sheetid: 'PreExamDataInExcel',
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'General Register Report | Date and Time: ' + DateAndTime,
            },
            
            columns: [
                
                { columnid: 'IndexId', title: 'Sr. No' },               
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'StudentName', title: 'Student Name(Surname FirstName MiddleName)' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'CorrespondenceAddress', title: 'Correspondence Address' },
                { columnid: 'MobileNo', title: 'Contact No' },
                { columnid: 'ApplicationReservationName', title: 'Category' },
                { columnid: 'Caste', title: 'Caste' },
                { columnid: 'PlaceOfBirth', title: 'Place of Birth' },
                { columnid: 'DOB', title: 'Date of Birth' },
                { columnid: 'PreviousSchoolCollegeName', title: 'Previous School/College Name' },
                { columnid: 'DateOfAdmission', title: 'Date of Admission' },
                { columnid: 'CourseName', title: 'Course Admitted to' },
            ],
           
        };

        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.GeneralRegister]);
    };
    
    
});