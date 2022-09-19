app.controller('StudentPartTermPaperReportCtrl', function ($scope, $http, $filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Part Term wise Student - Paper List";

    /*Reset Academic Year Level*/
    $scope.resetSPTP = function () {
        $scope.SPTP = {};
    };
    $scope.SPTPTableParams = new NgTableParams({
    }, {
        dataset: []

    });
    

    $scope.exportData = function () {
        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Part_Term_wise_Student-Paper_List" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Part Term wise Student - Paper List ' + DateAndTime,
            },
          
            columns: [
                { columnid: 'Index', title: 'Sr No.' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Semester Name' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'SeatNumber', title: 'Seat Number' },
                { columnid: 'FullName', title: 'Student Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'PaperName', title: 'Paper Name' },

            ],
        };

            //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.SPTPTabledata]);
        
    };





    /*Get Academic Year List*/
    $scope.getSPTPList = function () {

       
        $scope.SPTPTableParams = new NgTableParams({
        }, {
            dataset: []

        });
        $http({
            method: 'POST',
            url: 'api/StudentPartTermPaperList/GetStudentPartTermPaperReport',
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
                    $scope.SPTPTabledata = response.obj;
                    $scope.SPTPTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj

                    });
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

   

});

