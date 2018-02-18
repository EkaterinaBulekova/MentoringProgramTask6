using System;
using System.Xml;

namespace Lybrary.XML.Tests.FakeClasses
{
    internal class FakeXmlIdeal
    {
        public string XString { get; set; }
        public string X1 { get; set; }

        public FakeXmlIdeal()
        {
            this.XString =
                $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Catalog date=""{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}"" name=""First catalog"" number=""1"">
  <Element type=""FakeBook"">
    <Authors>
      <Author>Author1</Author>
      <Author>Author2</Author>
    </Authors>
    <Count>51</Count>
    <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
    <Title>First book</Title>
    <Note>some book</Note>
  </Element>
  <Element type=""FakePatent"">
    <Inventors>
      <Inventor>Inventor1</Inventor>
      <Inventor>Inventor2</Inventor>
    </Inventors>
    <Number>10</Number>
    <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
    <Title>First patent</Title>
    <Note>some patent</Note>
  </Element>
  <Element type=""FakeBook"">
    <Authors>
      <Author>Author1</Author>
      <Author>Author2</Author>
    </Authors>
    <Count>56</Count>
    <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
    <Title>Second book</Title>
    <Note>some book</Note>
  </Element>
  <Element type=""FakePatent"">
    <Inventors>
      <Inventor>Inventor1</Inventor>
      <Inventor>Inventor2</Inventor>
    </Inventors>
    <Number>10</Number>
    <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
    <Title>Second patent</Title>
    <Note>some patent</Note>
  </Element>
  <Element type=""FakeBook"">
    <Authors>
      <Author>Author1</Author>
      <Author>Author2</Author>
    </Authors>
    <Count>51</Count>
    <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
    <Title>Third book</Title>
    <Note>some book</Note>
  </Element>
  <Element type=""FakePatent"">
    <Inventors>
      <Inventor>Inventor1</Inventor>
      <Inventor>Inventor2</Inventor>
    </Inventors>
    <Number>10</Number>
    <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
    <Title>Third patent</Title>
    <Note>some patent</Note>
  </Element>
</Catalog>";
//            XString =
//                $@"<?xml version=""1.0"" encoding=""utf-8""?>
//<Catalog date=""{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}"" name=""First catalog"" number=""1"">
//    <Element type=""FakeBook"">
//        <Authors>
//            <Author>Author1</Author>
//            <Author>Author2</Author>
//        </Authors>
//        <Count>51</Count>
//        <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
//        <Title>First book</Title>
//        <Note>some book</Note>
//    </Element>
//    <Element type=""FakePatent"">
//        <Inventors>
//            <Inventor>Inventor1</Inventor>
//            <Inventor>Inventor2</Inventor>
//        </Inventors>
//        <Number>10</Number>
//        <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
//        <Title>First patent</Title>
//        <Note>some patent</Note>
//    </Element>
//    <Element type=""FakeBook"">
//        <Authors>
//            <Author>Author1</Author>
//            <Author>Author2</Author>
//        </Authors>
//        <Count>56</Count>
//        <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
//        <Title>Second book</Title>
//        <Note>some book</Note>
//    </Element>
//    <Element type=""FakePatent"">
//        <Inventors>
//            <Inventor>Inventor1</Inventor>
//            <Inventor>Inventor2</Inventor>
//        </Inventors>
//        <Number>10</Number>
//        <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
//        <Title>Second patent</Title>
//        <Note>some patent</Note>
//    </Element>
//    <Element type=""FakeBook"">
//        <Authors>
//            <Author>Author1</Author>
//            <Author>Author2</Author>
//        </Authors>
//        <Count>51</Count>
//        <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
//        <Title>Third book</Title>
//        <Note>some book</Note>
//    </Element>
//    <Element type=""FakePatent"">
//        <Inventors>
//            <Inventor>Inventor1</Inventor>
//            <Inventor>Inventor2</Inventor>
//        </Inventors>
//        <Number>10</Number>
//        <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
//        <Title>Third patent</Title>
//        <Note>some patent</Note>
//    </Element>
//</Catalog>";
        }
    }
}
