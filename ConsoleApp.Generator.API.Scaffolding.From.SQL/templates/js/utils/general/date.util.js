/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Date extenstion utililty class
|------------------------------------------------------------------------------------------------------------------
 */

const { appendLeadingZero, padLeft, padRight } = require(`./object.util`)();

/**
 * Creates a new DateUtil instance
 */
const DateUtil = () => {
  const dateParts = () => {
    var date = new Date();
    return {
      year: date.getFullYear(),
      month: appendLeadingZero(date.getMonth() + 1),
      day: appendLeadingZero(date.getDate()),
      hour: appendLeadingZero(date.getHours()),
      minutes: appendLeadingZero(date.getMinutes()),
      seconds: appendLeadingZero(date.getSeconds()),
      ms: padRight(padLeft(date.getMilliseconds(), `000`), `000`),
      dow: date.getDay(),
    };
  };
  const yyyymmdd = () => {
    const dp = dateParts();
    return [dp.year, dp.month, dp.day].join(``).trim();
  };
  const yyyymmddWithDashSeparator = () => {
    const dp = dateParts();
    return [dp.year, dp.month, dp.day].join(`-`).trim();
  };
  const yyyymmddWithDotSeparator = () => {
    const dp = dateParts();
    return [dp.year, dp.month, dp.day].join(`.`).trim();
  };
  const yyyymmddWithSlashSeparator = () => {
    const dp = dateParts();
    return [dp.year, dp.month, dp.day].join(`/`).trim();
  };
  const hms = () => {
    const dp = dateParts();
    return [dp.hour, dp.minutes, dp.seconds].join(`:`).trim();
  };
  const Thms = () => {
    return `T${hms()}`.trim();
  };
  const ThmsZ0200 = () => {
    return `${Thms()}+0200`.trim();
  };
  const hmsms = () => {
    const dp = dateParts();
    return [hms(), dp.ms].join(`.`).trim();
  };
  const Thmsms = () => {
    return `T${hmsms()}`.trim();
  };
  const ThmsmsZ0200 = () => {
    return `${Thmsms()}+0200`.trim();
  };
  const yyyymmddhms = () => {
    return [yyyymmdd(), hms()].join(` `).trim();
  };
  const yyyymmddThms = () => {
    return [yyyymmdd(), Thms()].join(` `).trim();
  };
  const yyyymmddThmsZ0200 = () => {
    return [yyyymmdd(), ThmsZ0200()].join(` `).trim();
  };
  const yyyymmddhmsms = () => {
    return [yyyymmdd(), hmsms()].join(` `).trim();
  };
  const yyyymmddThmsms = () => {
    return [yyyymmdd(), Thmsms()].join(``).trim();
  };
  const yyyymmddThmsmsZ0200 = () => {
    return [yyyymmdd(), ThmsmsZ0200()].join(``).trim();
  };
  const yyyymmddhmsWithDashSeparator = () => {
    return [yyyymmddWithDashSeparator(), hms()].join(` `).trim();
  };
  const yyyymmddThmsWithDashSeparator = () => {
    return [yyyymmddWithDashSeparator(), Thms()].join(` `).trim();
  };
  const yyyymmddThmsZ0200WithDashSeparator = () => {
    return [yyyymmddWithDashSeparator(), ThmsZ0200()].join(` `).trim();
  };
  const yyyymmddhmsmsWithDashSeparator = () => {
    return [yyyymmddWithDashSeparator(), hmsms()].join(` `).trim();
  };
  const yyyymmddThmsmsWithDashSeparator = () => {
    return [yyyymmddWithDashSeparator(), Thmsms()].join(``).trim();
  };
  const yyyymmddThmsmsZ0200WithDashSeparator = () => {
    return [yyyymmddWithDashSeparator(), ThmsmsZ0200()].join(``).trim();
  };
  const yyyymmddhmsWithDotSeparator = () => {
    return [yyyymmddWithDotSeparator(), hms()].join(` `).trim();
  };
  const yyyymmddThmsWithDotSeparator = () => {
    return [yyyymmddWithDotSeparator(), Thms()].join(` `).trim();
  };
  const yyyymmddThmsZ0200WithDotSeparator = () => {
    return [yyyymmddWithDotSeparator(), ThmsZ0200()].join(` `).trim();
  };
  const yyyymmddhmsmsWithDotSeparator = () => {
    return [yyyymmddWithDotSeparator(), hmsms()].join(` `).trim();
  };
  const yyyymmddThmsmsWithDotSeparator = () => {
    return [yyyymmddWithDotSeparator(), Thmsms()].join(``).trim();
  };
  const yyyymmddThmsmsZ0200WithDotSeparator = () => {
    return [yyyymmddWithDotSeparator(), ThmsmsZ0200()].join(``).trim();
  };
  const yyyymmddhmsWithSlashSeparator = () => {
    return [yyyymmddWithSlashSeparator(), hms()].join(` `).trim();
  };
  const yyyymmddThmsWithSlashSeparator = () => {
    return [yyyymmddWithSlashSeparator(), Thms()].join(` `).trim();
  };
  const yyyymmddThmsZ0200WithSlashSeparator = () => {
    return [yyyymmddWithSlashSeparator(), ThmsZ0200()].join(` `).trim();
  };
  const yyyymmddhmsmsWithSlashSeparator = () => {
    return [yyyymmddWithSlashSeparator(), hmsms()].join(` `).trim();
  };
  const yyyymmddThmsmsWithSlashSeparator = () => {
    return [yyyymmddWithSlashSeparator(), Thmsms()].join(``).trim();
  };
  const yyyymmddThmsmsZ0200WithSlashSeparator = () => {
    return [yyyymmddWithSlashSeparator(), ThmsmsZ0200()].join(``).trim();
  };
  const dateDiffInDays = (startDate, endDate) => {
    return (Date.parse(endDate) - Date.parse(startDate)) / 1000 / 60 / 60 / 24;
  };
  const dateDiffInHours = (startDate, endDate) => {
    return (Date.parse(endDate) - Date.parse(startDate)) / 1000 / 60 / 60;
  };
  const dateDiffInMinutes = (startDate, endDate) => {
    return (Date.parse(endDate) - Date.parse(startDate)) / 1000 / 60;
  };
  const dateDiffInSeconds = (startDate, endDate) => {
    return (Date.parse(endDate) - Date.parse(startDate)) / 1000;
  };
  const formatDateYYMMDDWithDashSeparator = (date) => {
    return [
      date.getFullYear(),
      appendLeadingZero(date.getMonth() + 1),
      appendLeadingZero(date.getDate()),
    ]
      .join(`-`)
      .trim();
  };
  const formatDateMMDDYYWithDashSeparator = (date) => {
    return [
      appendLeadingZero(date.getMonth() + 1),
      appendLeadingZero(date.getDate()),
      date.getFullYear(),
    ]
      .join(`-`)
      .trim();
  };
  const formatDateYYMMDDWithDotSeparator = (date) => {
    return [
      date.getFullYear(),
      appendLeadingZero(date.getMonth() + 1),
      appendLeadingZero(date.getDate()),
    ]
      .join(`.`)
      .trim();
  };
  const formatDateMMDDYYWithDotSeparator = (date) => {
    return [
      appendLeadingZero(date.getMonth() + 1),
      appendLeadingZero(date.getDate()),
      date.getFullYear(),
    ]
      .join(`.`)
      .trim();
  };
  const formatDateYYMMDDWithSlashSeparator = (date) => {
    return [
      date.getFullYear(),
      appendLeadingZero(date.getMonth() + 1),
      appendLeadingZero(date.getDate()),
    ]
      .join(`/`)
      .trim();
  };
  const formatDateMMDDYYWithSlashSeparator = (date) => {
    return [
      appendLeadingZero(date.getMonth() + 1),
      appendLeadingZero(date.getDate()),
      date.getFullYear(),
    ]
      .join(`/`)
      .trim();
  };
  const yyyymmddhmsmsDotAsTimestamp = () => {
    return new Date(yyyymmddhmsmsDotSeparator()).getTime() / 1000;
  };
  const toMidnightInHours = (date = new Date(), hours = 0) => {
    date.setHours(hours, 0, 0, 0);
    return date;
  };
  const DateDiff = {
    InYears: function (startDate, endDate) {
      return parseInt(endDate.getFullYear() - startDate.getFullYear());
    },
    InMonths: function (startDate, endDate) {
      let startDateYear = startDate.getFullYear();
      let endDateYear = endDate.getFullYear();
      let startDateMonth = startDate.getMonth() + 1;
      let endDateMonth = endDate.getMonth() + 1;
      return parseInt(
        endDateMonth + 12 * endDateYear - (startDateMonth + 12 * startDateYear)
      );
    },
    InWeeks: function (startDate, endDate) {
      let endDateTime = endDate.getTime();
      let startDateTime = startDate.getTime();
      return parseInt((endDateTime - startDateTime) / (24 * 3600 * 1000 * 7));
    },
    InDays: function (startDate, endDate) {
      return (
        parseInt(
          (Date.parse(endDate) - Date.parse(startDate)) / 1000 / 60 / 60 / 24
        ) * -1
      );
    },
    InHours: function (startDate, endDate) {
      return (
        parseInt(
          (Date.parse(endDate) - Date.parse(startDate)) / 1000 / 60 / 60
        ) * -1
      );
    },
    InMinutes: function (startDate, endDate) {
      return (
        parseInt((Date.parse(endDate) - Date.parse(startDate)) / 1000 / 60) * -1
      );
    },
    InSeconds: function (startDate, endDate) {
      return (
        parseInt((Date.parse(endDate) - Date.parse(startDate)) / 1000) * -1
      );
    },
    InMilliSeconds: function (startDate, endDate) {
      return parseInt(Date.parse(endDate) - Date.parse(startDate)) * -1;
    },
  };

  return {
    yyyymmdd,
    yyyymmddWithDashSeparator,
    yyyymmddWithDotSeparator,
    yyyymmddWithSlashSeparator,
    yyyymmddhms,
    yyyymmddThms,
    yyyymmddThmsZ0200,
    yyyymmddhmsms,
    yyyymmddThmsms,
    yyyymmddThmsmsZ0200,
    yyyymmddhmsWithDashSeparator,
    yyyymmddThmsWithDashSeparator,
    yyyymmddThmsZ0200WithDashSeparator,
    yyyymmddhmsmsWithDashSeparator,
    yyyymmddThmsmsWithDashSeparator,
    yyyymmddThmsmsZ0200WithDashSeparator,
    yyyymmddhmsWithDotSeparator,
    yyyymmddThmsWithDotSeparator,
    yyyymmddThmsZ0200WithDotSeparator,
    yyyymmddhmsmsWithDotSeparator,
    yyyymmddThmsmsWithDotSeparator,
    yyyymmddThmsmsZ0200WithDotSeparator,
    yyyymmddhmsWithSlashSeparator,
    yyyymmddThmsWithSlashSeparator,
    yyyymmddThmsZ0200WithSlashSeparator,
    yyyymmddhmsmsWithSlashSeparator,
    yyyymmddThmsmsWithSlashSeparator,
    yyyymmddThmsmsZ0200WithSlashSeparator,
    dateDiffInDays,
    dateDiffInHours,
    dateDiffInMinutes,
    dateDiffInSeconds,
    formatDateYYMMDDWithDashSeparator,
    formatDateMMDDYYWithDashSeparator,
    formatDateYYMMDDWithDotSeparator,
    formatDateMMDDYYWithDotSeparator,
    formatDateYYMMDDWithSlashSeparator,
    formatDateMMDDYYWithSlashSeparator,
    yyyymmddhmsmsDotAsTimestamp,
    toMidnightInHours,
    DateDiff,
  };
};

module.exports = DateUtil;
