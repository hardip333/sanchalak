app.controller('AdmCancelApplicationCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Application Cancel Form";
    $scope.obj = {};
    $scope.IsBackBtn = false;
    $scope.backToList = function () {
        $scope.showFormFlag = false;
        $scope.IsBackBtn = false;
        $("#dropAppStatus").val = "";
    }

    $scope.resetCancelForm = function () {
        $scope.ApplicationStatus = {};   
     
    }

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.expand_row = function (id) {

        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    }
    $scope.CancelList = [
        {
            Id: '1',
            name: 'Cancel Request Approved'
        }, {
            Id: '2',
            name: 'Cancel Request Rejected'

        },
        {
            Id: '3',
            name:'Pending'
        }
        
    ];

  


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

                $scope.getCancelledRequestList();
                console.log($scope.Institute);
            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.getCancelledRequestList = function () {
      
        $scope.obj = {};
        $scope.obj.InstituteId = $scope.Institute.InstituteId;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/AdmApplicationCancelForm/StudentCancelRequestGetByInstituteId',
            data: $scope.obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    $scope.NoRecLabel = true;
                    $scope.offSpinner();
                }
                else {
                  
                    $scope.RequestList = response.obj;
                    $scope.NoRecLabel = false;
                        $scope.CancelTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    $scope.offSpinner();
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Update CentralAdmission*/
    $scope.updateCancelFormRequest = function () {
        $scope.CancelFaculty.ApplicationStatus = $scope.ApplicationStatus.name;
      
        var flag = true;
        if ($scope.CancelFaculty.IsAdmissionFeePaid == 'Yes' && $scope.CancelFaculty.ApplicationStatus =="Cancel Request Approved" ) {
            if ($scope.CancelFaculty.FacultyRemark == null || $scope.CancelFaculty.FacultyRemark == undefined) {
                alert("Please type Faculty Remark!!!");
                flag = false;
            }

            $scope.CancelFaculty.ApplicationStatus = 'Cancel Request Approved-Forward To Academic'


        }
        if ($scope.CancelFaculty.IsAdmissionFeePaid == 'Yes' && $scope.CancelFaculty.ApplicationStatus == "Cancel Request Rejected") {
            if ($scope.CancelFaculty.FacultyRemark == null || $scope.CancelFaculty.FacultyRemark == undefined) {
                alert("Please type Faculty Remark!!!");
                flag = false;
            }

            $scope.CancelFaculty.ApplicationStatus = 'Cancel Request Rejected'


        }
        if ($scope.CancelFaculty.IsAdmissionFeePaid == 'Yes' && $scope.CancelFaculty.ApplicationStatus == "Pending") {
            if ($scope.CancelFaculty.FacultyRemark == null || $scope.CancelFaculty.FacultyRemark == undefined) {
                alert("Please type Faculty Remark!!!");
                flag = false;
            }

            $scope.CancelFaculty.ApplicationStatus = 'Pending'


        }
     
        if ($scope.CancelFaculty.IsAdmissionFeePaid == 'No' && $scope.CancelFaculty.ApplicationStatus == "Cancel Request Approved") {
            
            if ($scope.CancelFaculty.FacultyRemark === null || $scope.CancelFaculty.FacultyRemark === undefined ||
                $scope.ApplicationStatus === null || $scope.ApplicationStatus === undefined) {
                alert("please check all fields !!!");
                flag = false;
            }

            $scope.CancelFaculty.ApplicationStatus = 'Cancel Request Approved'

            
        }

        if ($scope.CancelFaculty.IsAdmissionFeePaid == 'No' && $scope.CancelFaculty.ApplicationStatus == "Cancel Request Rejected") {

            if ($scope.CancelFaculty.FacultyRemark === null || $scope.CancelFaculty.FacultyRemark === undefined ||
                $scope.ApplicationStatus === null || $scope.ApplicationStatus === undefined) {
                alert("please check all fields !!!");
                flag = false;
            }

            $scope.CancelFaculty.ApplicationStatus = 'Cancel Request Rejected'


        }

       
        if (flag == true) {
           /* var obj = {};
            var obj =
            {
                Id: $scope.CancelFaculty.Id,
                FacultyRemark: $scope.CancelFaculty.FacultyRemark,
                ApplicationStatus: $scope.CancelFaculty.ApplicationStatus,
                ApplicationId: $scope.CancelFaculty.ApplicationId
               

            };*/
            $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/AdmApplicationCancelForm/AdmApplicationCancelFacultyUpdate',
                    data: $scope.CancelFaculty,
                    headers: { "Content-Type": 'application/json' }
                })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.showFormFlag = false;
                        $scope.getCancelledRequestList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });}
           
        
    };

    $scope.modifyCancelData = function (data) {
       
      /*  if (data.ApplicationStatus == 'Cancel Request Approved-Forward To Academic') {
            [{ Id: '1',
                name: 'Cancel Request Approved-Forward To Academic',
                selected: true
            }]
        }
        else if (data.ApplicationStatus == 'Cancel Request Rejected') {
            [{
                Id: '2',
                name: 'Cancel Request Rejected',
                selected: true
            }]
        }
        else if (data.ApplicationStatus == 'Pending') {
            [{
                Id: '3',
                name: 'Pending',
                selected: true
            }];
        }
        else if (data.ApplicationStatus == 'Requested') {
            [{
                Id: '4',
                name: '',
                selected:true

            }];
        }*/
       
        if (data.RemarkStatus == "Status Updated") {
          
            $scope.CancelFaculty = data;
            $scope.ApplicationStatus = data.ApplicationStatus;
            $scope.showFormFlag = true;
            $scope.IsBackBtn = true;
        }
        else {
            $scope.CancelFaculty = data;
            $scope.CancelFaculty.FacultyRemark = null;
            $scope.showFormFlag = true;
            $scope.IsBackBtn = true;
        }
        $(window).scrollTop(0);
       
    };


    $scope.exportCancelDataFaculty = function () {

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "CancelApplicationFormDetails" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'CancelApplicationFormDetails| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationId', title: 'Application Form No' },
                { columnid: 'ApplicantUserName', title: 'User Name' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'ApplicationStatus', title: 'Application Status' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'Reason', title: 'Applicant Reason' },
                { columnid: 'FacultyRemark', title: 'Faculty Remark' },





            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.RequestList]);
    };

   
   
});