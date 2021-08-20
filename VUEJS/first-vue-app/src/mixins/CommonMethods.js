export default {
    methods: {
        /**
         * Format dữ liệu ngày tháng sang định dạng khác mong muốn
         * seperator = "-" : year/month/day
         * seperator = "/" : day/month/year
         * @param {string} dateString xâu dạng date
         * @param {string} seperator dấu phân cách để chia theo định dạng
         * @returns xâu rỗng hoặc xâu dạng date theo định dạng
         * Created by: nvdien (20/7/2021)
         */

        formatDate(dateString, seperator) {
            let dateObj = new Date(dateString);
            if (Number.isNaN(dateObj.getTime())) {
                return "";
            } else {
                let month = dateObj.getUTCMonth() + 1;
                let day = dateObj.getUTCDate() + 1;
                let year = dateObj.getUTCFullYear();
                if (month < 10) {
                    month = "0" + month;
                }
                if (day < 10) {
                    day = "0" + day;
                }
                let newdate = "";
                if (seperator == "-") {
                    newdate = year + seperator + month + seperator + day;
                }
                if (seperator == "/") {
                    newdate = day + seperator + month + seperator + year;
                }
                return newdate;
            }
        },

        /**
         * chuyển từ dạng số sang dạng ngăn cách bởi dấu '.'
         * @param {string} money string tiền tệ
         * @returns string tiền tệ theo đúng định dạng
         * Created by nvdien (20/7/2021)
         */
        formatMoney(money) {
            if (money) {
                return money.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ".");
            }
            return "";
        },

        /**
         * chuyển từ dạng ngăn cách bởi dấu '.' sang dạng số
         * @param {string} money 
         * author nvdien(26/7/2021)
         */
        formatMoneytoDouble(money) {
            return money.split('.').join('');
        },
    }
}